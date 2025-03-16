namespace ServiceContracts
{
    using Shared.Response.MovimientoController;
    using System;
    using System.Threading.Tasks;

    public interface IMovimientoService
    {
        Task<ResponseOfGetMovimientos.Movimiento?> GetMovimientoAsync(int id);
        Task<ResponseOfGetMovimientos?> GetMovimientosAsync(DateOnly? fechaDesde);
    }
}
