namespace RepositoryContracts
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICuentaRepository
    {
        Task<Cuentum?> GetCuentaByIdAsync(int id);
        IQueryable<Cuentum>? GetCuentas();
        void UpdateCuenta(Cuentum cuenta);
        void UpdateCuentas(Cuentum[] cuentas);
    }
}
