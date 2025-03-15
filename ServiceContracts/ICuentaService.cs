namespace ServiceContracts
{
    using Shared.Response.CuentaController;
    using System.Threading.Tasks;

    public interface ICuentaService
    {
        Task<ResponseOfGetCuentas.Cuenta?> GetCuentaByIdAsync(int id);
        ResponseOfGetCuentas GetCuentas();
        Task CloseCuentaByIdAsync(int id);
    }
}
