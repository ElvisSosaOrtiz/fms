namespace Services.Mapper
{
    using Entities;
    using Shared.Enums;
    using Shared.Response.TransaccionController;

    internal static class TransaccionMappers
    {
        internal static ResponseOfGetTransacciones.Transaccion MapTransaccionResponse(Transaccion transaccion)
        {
            return new()
            {
                Id = transaccion.IdTransaccion,
                CuentaOrigen = new()
                {
                    Id = transaccion.IdCuentaOrigen!.Value,
                    Nombre = transaccion.IdCuentaOrigenNavigation!.Nombre!,
                    Balance = transaccion.IdCuentaOrigenNavigation!.Balance!.Value,
                    Tipo = (TiposCuenta)transaccion.IdCuentaOrigenNavigation.IdTipo!,
                    Estado = (EstadosCuenta)transaccion.IdCuentaOrigenNavigation.IdEstado!,
                },
                CuentaDestino = new()
                {
                    Id = transaccion.IdCuentaDestino!.Value,
                    Nombre = transaccion.IdCuentaDestinoNavigation!.Nombre!,
                    Balance = transaccion.IdCuentaDestinoNavigation!.Balance!.Value,
                    Tipo = (TiposCuenta)transaccion.IdCuentaDestinoNavigation.IdTipo!,
                    Estado = (EstadosCuenta)transaccion.IdCuentaDestinoNavigation.IdEstado!,
                },
                Monto = transaccion.Monto!.Value,
                Descripcion = transaccion.Descripcion,
                Fecha = transaccion.Fecha!.Value
            };
        }
    }
}
