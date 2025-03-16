namespace Services
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using RepositoryContracts;
    using ServiceContracts;
    using Shared.Enums;
    using Shared.Response.CuentaController;

    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly ILogger<CuentaService> _logger;

        public CuentaService(
            ICuentaRepository cuentaRepository,
            ILogger<CuentaService> logger)
        {
            _cuentaRepository = cuentaRepository;
            _logger = logger;
        }

        public async Task<ResponseOfGetCuentas?> GetCuentasAsync()
        {
            try
            {
                var cuentas = await _cuentaRepository.GetCuentas()!.ToListAsync();

                if (cuentas is null || cuentas.Count == 0) return new();

                return new()
                {
                    Cuentas = cuentas.Select(cuenta => new ResponseOfGetCuentas.Cuenta()
                    {
                        Id = cuenta.IdCuenta,
                        Nombre = cuenta.Nombre!,
                        Balance = cuenta.Balance!.Value,
                        Tipo = cuenta.IdTipoNavigation!.Nombre,
                        Estado = cuenta.IdEstadoNavigation!.Nombre
                    })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<ResponseOfGetCuentas.Cuenta?> GetCuentaByIdAsync(int id)
        {
            try
            {
                var cuenta = await _cuentaRepository.GetCuentaByIdAsync(id);

                if (cuenta is null)
                {
                    _logger.LogError("Could not find account");
                    return null;
                }

                return new()
                {
                    Id = cuenta.IdCuenta,
                    Nombre = cuenta.Nombre!,
                    Balance = cuenta.Balance!.Value,
                    Tipo = cuenta.IdTipoNavigation!.Nombre,
                    Estado = cuenta.IdEstadoNavigation!.Nombre
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task CloseCuentaByIdAsync(int id)
        {
            try
            {
                var cuenta = await _cuentaRepository.GetCuentaByIdAsync(id);

                if (cuenta is null)
                {
                    _logger.LogError("Could not find account to close");
                    return;
                }

                cuenta.IdEstado = (int)EstadosCuenta.Cerrada;

                await _cuentaRepository.UpdateCuentaAsync(cuenta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
