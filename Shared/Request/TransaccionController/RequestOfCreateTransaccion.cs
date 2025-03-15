namespace Shared.Request.TransaccionController
{
    public class RequestOfCreateTransaccion
    {
        public int IdCuentaOrigen { get; set; }
        public int IdCuentaDestino { get; set; }
        public double Monto { get; set; }
        public string? Descripcion { get; set; }
    }
}
