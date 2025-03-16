namespace RepositoryContracts
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMovimientoRepository
    {
        Task CreateMovimientoAsync(Movimiento movimiento);
        Task<Movimiento?> GetMovimientoAsync(int id);
        IQueryable<Movimiento> GetMovimientos();
    }
}
