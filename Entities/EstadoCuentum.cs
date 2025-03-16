using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class EstadoCuentum
{
    [Key]
    [Column("id_estado")]
    public int IdEstado { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
