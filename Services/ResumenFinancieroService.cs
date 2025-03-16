namespace Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using RepositoryContracts;
    using ServiceContracts;
    using Shared.Enums;
    using Shared.Response.ResumenFinancieroController;

    public class ResumenFinancieroService : IResumenFinancieroService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly ILogger<ResumenFinancieroService> _logger;

        public ResumenFinancieroService(
            ICuentaRepository cuentaRepository,
            ILogger<ResumenFinancieroService> logger)
        {
            _cuentaRepository = cuentaRepository;
            _logger = logger;
        }

        public async Task<ResponseOfGetResumenFinanciero?> GetResumenFinancieroAsync()
        {
            try
            {
                var cuentas = await _cuentaRepository.GetCuentas()
                    .Where(cuenta => cuenta.IdEstado != (int)EstadosCuenta.Cerrada)
                    .ToListAsync();

                if (cuentas.Count == 0) return null;

                return new()
                {
                    Cuentas = cuentas.Select(cuenta => new ResponseOfGetResumenFinanciero.Cuenta
                    {
                        Nombre = cuenta.Nombre!,
                        Balance = cuenta.Balance!.Value
                    }),
                    PresupuestoTotal = cuentas.Sum(cuenta => cuenta.Balance!.Value)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
