namespace Shared.Response.TransaccionController
{
    using Shared.Enums;

    public class ResponseOfGetTransacciones
    {
        public IEnumerable<Transaccion> Transacciones { get; set; } = [];

        public class Transaccion
        {
            public int Id { get; set; }
            public Cuenta CuentaOrigen { get; set; } = null!;
            public Cuenta CuentaDestino { get; set; } = null!;
            public double Monto { get; set; }
            public string? Descripcion { get; set; }
            public DateOnly Fecha { get; set; }
        }

        public class Cuenta
        {
            public int Id { get; set; }
            public TiposCuenta Tipo { get; set; }
            public EstadosCuenta Estado { get; set; }
            public string Nombre { get; set; } = null!;
            public double Balance { get; set; }
        }
    }
}
