namespace Services
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using RepositoryContracts;
    using ServiceContracts;
    using Services.Mappers;
    using Shared.Response.MovimientoController;

    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ILogger<MovimientoService> _logger;

        public MovimientoService(
            IMovimientoRepository movimientoRepository,
            ILogger<MovimientoService> logger)
        {
            _movimientoRepository = movimientoRepository;
            _logger = logger;
        }

        public async Task<ResponseOfGetMovimientos?> GetMovimientosAsync(DateOnly? fechaDesde)
        {
            try
            {
                var movimientos = _movimientoRepository.GetMovimientos();

                var movimientosList = new List<Movimiento>();

                if (fechaDesde.HasValue)
                {
                    movimientosList = await movimientos
                        .Where(mv => mv.Fecha!.Value >= fechaDesde.Value)
                        .ToListAsync();
                }
                else
                {
                    movimientosList = await movimientos
                        .Where(mv => mv.Fecha!.Value >= DateOnly.FromDateTime(DateTime.Now.AddDays(-90)))
                        .ToListAsync();
                }

                if (movimientosList is null || movimientosList.Count == 0) return new();

                return new() { Movimientos = movimientosList.Select(mv => mv.MapMovimientoResponse()) };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<ResponseOfGetMovimientos.Movimiento?> GetMovimientoAsync(int id)
        {
            try
            {
                var movimiento = await _movimientoRepository.GetMovimientoAsync(id);

                if (movimiento is null)
                {
                    _logger.LogError("Could not find transaction movement");
                    return null;
                }

                return movimiento.MapMovimientoResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
