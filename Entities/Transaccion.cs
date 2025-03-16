using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

[Table("Transaccion")]
public partial class Transaccion
{
    [Key]
    [Column("id_transaccion")]
    public int IdTransaccion { get; set; }

    [Column("id_cuenta_origen")]
    public int? IdCuentaOrigen { get; set; }

    [Column("id_cuenta_destino")]
    public int? IdCuentaDestino { get; set; }

    [Column("monto")]
    public double? Monto { get; set; }

    [Column("descripcion")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("fecha")]
    public DateOnly? Fecha { get; set; }

    [ForeignKey("IdCuentaDestino")]
    [InverseProperty("TransaccionIdCuentaDestinoNavigations")]
    public virtual Cuentum? IdCuentaDestinoNavigation { get; set; }

    [ForeignKey("IdCuentaOrigen")]
    [InverseProperty("TransaccionIdCuentaOrigenNavigations")]
    public virtual Cuentum? IdCuentaOrigenNavigation { get; set; }
}
