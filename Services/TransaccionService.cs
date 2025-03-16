namespace Services
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using RepositoryContracts;
    using ServiceContracts;
    using Services.Mappers;
    using Shared.Enums;
    using Shared.Request.TransaccionController;
    using Shared.Response.TransaccionController;

    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ILogger<TransaccionService> _logger;

        public TransaccionService(
            ITransaccionRepository transaccionRepository,
            ICuentaRepository cuentaRepository,
            IMovimientoRepository movimientoRepository,
            ILogger<TransaccionService> logger)
        {
            _transaccionRepository = transaccionRepository;
            _cuentaRepository = cuentaRepository;
            _movimientoRepository = movimientoRepository;
            _logger = logger;
        }

        public async Task<ResponseOfGetTransacciones?> GetTransaccionesAsync(DateOnly fechaDesde, DateOnly fechaHasta)
        {
            try
            {
                var transacciones = await _transaccionRepository.GetTransacciones()
                    .Where(tr => tr.Fecha!.Value >= fechaDesde && tr.Fecha!.Value <= fechaHasta)
                    .ToListAsync();

                if (transacciones is null || transacciones.Count == 0) return new();

                return new() { Transacciones = transacciones.Select(tr => tr.MapTransaccionResponse()) };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<ResponseOfGetTransacciones.Transaccion?> CreateTransaccionAsync(RequestOfCreateTransaccion request)
        {
            try
            {
                var cuentaOrigen = await _cuentaRepository.GetCuentaByIdAsync(request.IdCuentaOrigen);
                var cuentaDestino = await _cuentaRepository.GetCuentaByIdAsync(request.IdCuentaDestino);

                if (cuentaOrigen is null || cuentaDestino is null)
                {
                    _logger.LogError("One or neither of the accounts could not be found");
                    return null;
                }

                if ((EstadosCuenta)cuentaOrigen!.IdEstado! == EstadosCuenta.Cerrada ||
                    (EstadosCuenta)cuentaDestino!.IdEstado! == EstadosCuenta.Cerrada)
                {
                    _logger.LogError("One or both of the accounts are closed");
                    return null;
                }

                cuentaOrigen!.Balance -= request.Monto;
                cuentaDestino!.Balance += request.Monto;

                var nowDate = DateOnly.Parse(DateTime.Now.ToShortDateString());

                var transaccion = new Transaccion
                {
                    IdCuentaOrigen = request.IdCuentaOrigen,
                    IdCuentaDestino = request.IdCuentaDestino,
                    Monto = request.Monto,
                    Descripcion = request.Descripcion,
                    Fecha = nowDate
                };

                var movimiento = new Movimiento
                {
                    IdCuentaOrigen = request.IdCuentaOrigen,
                    IdCuentaDestino = request.IdCuentaDestino,
                    Monto = request.Monto,
                    Fecha = nowDate
                };

                var createdTransaccion = await _transaccionRepository.CreateTransaccionAsync(transaccion);
                await _cuentaRepository.UpdateCuentasAsync([cuentaOrigen, cuentaDestino]);
                await _movimientoRepository.CreateMovimientoAsync(movimiento);

                return createdTransaccion.MapTransaccionResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
