namespace Services.Mappers
{
    using Entities;
    using Shared.Enums;
    using Shared.Response.MovimientoController;
    using Shared.Response.TransaccionController;

    internal static class ServiceMappers
    {
        internal static ResponseOfGetTransacciones.Transaccion MapTransaccionResponse(this Transaccion transaccion)
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

        internal static ResponseOfGetMovimientos.Movimiento MapMovimientoResponse(this Movimiento movimiento)
        {
            return new()
            {
                Id = movimiento.IdMovimiento,
                CuentaOrigen = movimiento.IdCuentaOrigenNavigation!.Nombre!,
                CuentaDestino = movimiento.IdCuentaDestinoNavigation!.Nombre!,
                Monto = movimiento.Monto!.Value,
                Fecha = movimiento.Fecha!.Value
            };
        }
    }
}
