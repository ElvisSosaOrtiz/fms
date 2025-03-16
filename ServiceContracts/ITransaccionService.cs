namespace ServiceContracts
{
    using Shared.Request.TransaccionController;
    using Shared.Response.TransaccionController;
    using System;
    using System.Threading.Tasks;

    public interface ITransaccionService
    {
        Task<ResponseOfGetTransacciones.Transaccion?> CreateTransaccionAsync(RequestOfCreateTransaccion request);
        Task<ResponseOfGetTransacciones?> GetTransaccionesAsync(DateOnly fechaDesde, DateOnly fechaHasta);
    }
}
