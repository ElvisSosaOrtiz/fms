namespace RepositoryContracts
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICuentaRepository
    {
        Task<Cuentum?> GetCuentaByIdAsync(int id);
        IQueryable<Cuentum> GetCuentas();
        Task UpdateCuentaAsync(Cuentum cuenta);
        Task UpdateCuentasAsync(Cuentum[] cuentas);
    }
}
