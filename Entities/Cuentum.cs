using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class Cuentum
{
    [Key]
    [Column("id_cuenta")]
    public int IdCuenta { get; set; }

    [Column("id_tipo")]
    public int? IdTipo { get; set; }

    [Column("id_estado")]
    public int? IdEstado { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("balance")]
    public double? Balance { get; set; }

    [ForeignKey("IdEstado")]
    [InverseProperty("Cuenta")]
    public virtual EstadoCuentum? IdEstadoNavigation { get; set; }

    [ForeignKey("IdTipo")]
    [InverseProperty("Cuenta")]
    public virtual TipoCuentum? IdTipoNavigation { get; set; }

    [InverseProperty("IdCuentaDestinoNavigation")]
    public virtual ICollection<Movimiento> MovimientoIdCuentaDestinoNavigations { get; set; } = new List<Movimiento>();

    [InverseProperty("IdCuentaOrigenNavigation")]
    public virtual ICollection<Movimiento> MovimientoIdCuentaOrigenNavigations { get; set; } = new List<Movimiento>();

    [InverseProperty("IdCuentaDestinoNavigation")]
    public virtual ICollection<Transaccion> TransaccionIdCuentaDestinoNavigations { get; set; } = new List<Transaccion>();

    [InverseProperty("IdCuentaOrigenNavigation")]
    public virtual ICollection<Transaccion> TransaccionIdCuentaOrigenNavigations { get; set; } = new List<Transaccion>();
}
