namespace RepositoryContracts
{
    using Entities;
    using System.Linq;

    public interface ITransaccionRepository
    {
        Task<Transaccion> CreateTransaccionAsync(Transaccion transaccion);
        IQueryable<Transaccion> GetTransacciones();
    }
}
