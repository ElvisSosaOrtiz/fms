namespace Services
{
    using Entities;
    using Microsoft.Extensions.Logging;
    using RepositoryContracts;
    using ServiceContracts;
    using Services.Mapper;
    using Shared.Enums;
    using Shared.Request.TransaccionController;
    using Shared.Response.TransaccionController;

    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly ILogger<TransaccionService> _logger;

        public TransaccionService(
            ITransaccionRepository transaccionRepository,
            ICuentaRepository cuentaRepository,
            ILogger<TransaccionService> logger)
        {
            _transaccionRepository = transaccionRepository;
            _cuentaRepository = cuentaRepository;
            _logger = logger;
        }

        public ResponseOfGetTransacciones GetTransacciones(DateOnly fechaDesde, DateOnly fechaHasta)
        {
            try
            {
                var transacciones = _transaccionRepository.GetTransacciones();

                if (transacciones is null || !transacciones.Any()) return new();

                return new() 
                {
                    Transacciones = transacciones
                        .Where(tr => tr.Fecha!.Value >= fechaDesde && tr.Fecha!.Value <= fechaHasta)
                        .Select(tr => TransaccionMappers.MapTransaccionResponse(tr))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
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

                var transaccion = new Transaccion
                {
                    IdCuentaOrigen = request.IdCuentaOrigen,
                    IdCuentaDestino = request.IdCuentaDestino,
                    Monto = request.Monto,
                    Descripcion = request.Descripcion,
                    Fecha = DateOnly.Parse(DateTime.Now.ToShortDateString())
                };

                var createdTransaccion = _transaccionRepository.CreateTransaccion(transaccion);
                _cuentaRepository.UpdateCuentas([cuentaOrigen, cuentaDestino]);

                return TransaccionMappers.MapTransaccionResponse(createdTransaccion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
