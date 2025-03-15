using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

[Table("Movimiento")]
public partial class Movimiento
{
    [Key]
    [Column("id_movimiento")]
    public int IdMovimiento { get; set; }

    [Column("id_cuenta_origen")]
    public int? IdCuentaOrigen { get; set; }

    [Column("id_cuenta_destino")]
    public int? IdCuentaDestino { get; set; }

    [Column("monto")]
    public double? Monto { get; set; }

    [ForeignKey("IdCuentaDestino")]
    [InverseProperty("MovimientoIdCuentaDestinoNavigations")]
    public virtual Cuentum? IdCuentaDestinoNavigation { get; set; }

    [ForeignKey("IdCuentaOrigen")]
    [InverseProperty("MovimientoIdCuentaOrigenNavigations")]
    public virtual Cuentum? IdCuentaOrigenNavigation { get; set; }
}
