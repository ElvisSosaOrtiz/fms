namespace Repositories
{
    using AppDbContext;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using RepositoryContracts;

    public class CuentaRepository : ICuentaRepository
    {
        private readonly MinisterioEducacionDfContext _dbContext;

        public CuentaRepository(MinisterioEducacionDfContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Cuentum> GetCuentas()
        {
            return _dbContext.Cuenta
                .Include(prop => prop.IdTipoNavigation)
                .Include(prop => prop.IdEstadoNavigation);
        }

        public async Task<Cuentum?> GetCuentaByIdAsync(int id)
        {
            return await _dbContext.Cuenta
                .Include(prop => prop.IdTipoNavigation)
                .Include(prop => prop.IdEstadoNavigation)
                .FirstOrDefaultAsync(cuenta => cuenta.IdCuenta == id);
        }

        public async Task UpdateCuentaAsync(Cuentum cuenta)
        {
            _dbContext.Cuenta.Update(cuenta);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCuentasAsync(Cuentum[] cuentas)
        {
            _dbContext.Cuenta.UpdateRange(cuentas);
            await _dbContext.SaveChangesAsync();
        }
    }
}
