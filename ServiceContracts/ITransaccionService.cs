namespace ServiceContracts
{
    using Shared.Request.TransaccionController;
    using Shared.Response.TransaccionController;
    using System;
    using System.Threading.Tasks;

    public interface ITransaccionService
    {
        Task<ResponseOfGetTransacciones.Transaccion?> CreateTransaccionAsync(RequestOfCreateTransaccion request);
        ResponseOfGetTransacciones GetTransacciones(DateOnly fechaDesde, DateOnly fechaHasta);
    }
}
