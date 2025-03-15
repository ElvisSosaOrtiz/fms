namespace Repositories
{
    using AppDbContext;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using RepositoryContracts;

    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly MinisterioEducacionDfContext _dbContext;

        public TransaccionRepository(MinisterioEducacionDfContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Transaccion> GetTransacciones() => _dbContext.Transaccions
            .Include(prop => prop.IdCuentaOrigenNavigation)
            .Include(prop => prop.IdCuentaDestinoNavigation);

        public Transaccion CreateTransaccion(Transaccion transaccion)
        {
            _dbContext.Transaccions.Add(transaccion);
            _dbContext.SaveChanges();

            return transaccion;
        }
    }
}
