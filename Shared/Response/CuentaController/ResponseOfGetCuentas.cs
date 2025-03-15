namespace Shared.Response.CuentaController
{
    public class ResponseOfGetCuentas
    {
        public IEnumerable<Cuenta> Cuentas { get; set; } = [];

        public class Cuenta
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = null!;
            public double Balance { get; set; }
            public string Tipo { get; set; } = null!;
            public string Estado { get; set; } = null!;
        }
    }
}
