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

        public async Task<Transaccion> CreateTransaccionAsync(Transaccion transaccion)
        {
            await _dbContext.Transaccions.AddAsync(transaccion);
            await _dbContext.SaveChangesAsync();

            return transaccion;
        }
    }
}
