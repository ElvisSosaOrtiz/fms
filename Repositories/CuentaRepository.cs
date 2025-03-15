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

        public IQueryable<Cuentum>? GetCuentas()
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

        public void UpdateCuenta(Cuentum cuenta)
        {
            _dbContext.Cuenta.Update(cuenta);
            _dbContext.SaveChanges();
        }

        public void UpdateCuentas(Cuentum[] cuentas)
        {
            _dbContext.Cuenta.UpdateRange(cuentas);
            _dbContext.SaveChanges();
        }
    }
}
