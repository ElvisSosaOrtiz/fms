namespace ServiceContracts
{
    using Shared.Response.CuentaController;
    using System.Threading.Tasks;

    public interface ICuentaService
    {
        Task<ResponseOfGetCuentas.Cuenta?> GetCuentaByIdAsync(int id);
        Task<ResponseOfGetCuentas?> GetCuentasAsync();
        Task CloseCuentaByIdAsync(int id);
    }
}
