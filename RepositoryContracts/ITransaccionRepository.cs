namespace RepositoryContracts
{
    using Entities;
    using System.Linq;

    public interface ITransaccionRepository
    {
        Transaccion CreateTransaccion(Transaccion transaccion);
        IQueryable<Transaccion> GetTransacciones();
    }
}
