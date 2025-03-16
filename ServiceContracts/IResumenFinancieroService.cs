namespace ServiceContracts
{
    using Shared.Response.ResumenFinancieroController;
    using System.Threading.Tasks;

    public interface IResumenFinancieroService
    {
        Task<ResponseOfGetResumenFinanciero?> GetResumenFinancieroAsync();
    }
}
