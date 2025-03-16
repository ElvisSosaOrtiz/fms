namespace Repositories
{
    using AppDbContext;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using RepositoryContracts;

    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly MinisterioEducacionDfContext _dbContext;

        public MovimientoRepository(MinisterioEducacionDfContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Movimiento> GetMovimientos()
        {
            return _dbContext.Movimientos
                .Include(prop => prop.IdCuentaOrigenNavigation)
                .Include(prop => prop.IdCuentaDestinoNavigation);
        }

        public async Task<Movimiento?> GetMovimientoAsync(int id)
        {
            return await _dbContext.Movimientos
                .Include(prop => prop.IdCuentaOrigenNavigation)
                .Include(prop => prop.IdCuentaDestinoNavigation)
                .FirstOrDefaultAsync(mv => mv.IdMovimiento == id);
        }

        public async Task CreateMovimientoAsync(Movimiento movimiento)
        {
            await _dbContext.Movimientos.AddAsync(movimiento);
            await _dbContext.SaveChangesAsync();
        }
    }
}
