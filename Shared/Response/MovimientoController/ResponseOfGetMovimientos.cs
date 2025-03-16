namespace Shared.Response.MovimientoController
{
    public class ResponseOfGetMovimientos
    {
        public IEnumerable<Movimiento> Movimientos { get; set; } = [];

        public class Movimiento
        {
            public int Id { get; set; }
            public string CuentaOrigen { get; set; } = null!;
            public string CuentaDestino { get; set; } = null!;
            public double Monto { get; set; }
            public DateOnly Fecha { get; set; }
        }
    }
}
