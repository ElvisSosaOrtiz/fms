namespace Shared.Response.ResumenFinancieroController
{
    public class ResponseOfGetResumenFinanciero
    {
        public IEnumerable<Cuenta> Cuentas { get; set; } = [];
        public double PresupuestoTotal { get; set; }

        public class Cuenta
        {
            public string Nombre { get; set; } = null!;
            public double Balance { get; set; }
        }
    }
}
