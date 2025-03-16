using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class TipoCuentum
{
    [Key]
    [Column("id_tipo")]
    public int IdTipo { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoNavigation")]
    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
