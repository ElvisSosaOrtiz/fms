using Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDbContext;

public partial class MinisterioEducacionDfContext : DbContext
{
    public MinisterioEducacionDfContext()
    {
    }

    public MinisterioEducacionDfContext(DbContextOptions<MinisterioEducacionDfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<EstadoCuentum> EstadoCuenta { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5QQG7I2\\SQLEXPRESS;Database=MinisterioEducacion_DF;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PK__Cuenta__C7E2868565DCC138");

            entity.Property(e => e.IdCuenta).ValueGeneratedNever();

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Cuenta).HasConstraintName("FK__Cuenta__id_estad__4E88ABD4");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Cuenta).HasConstraintName("FK__Cuenta__id_tipo__4D94879B");
        });

        modelBuilder.Entity<EstadoCuentum>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__EstadoCu__86989FB2F051EF6E");

            entity.Property(e => e.IdEstado).ValueGeneratedNever();
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__2A071C24D2FBA4AB");

            entity.HasOne(d => d.IdCuentaDestinoNavigation).WithMany(p => p.MovimientoIdCuentaDestinoNavigations).HasConstraintName("FK__Movimient__id_cu__03F0984C");

            entity.HasOne(d => d.IdCuentaOrigenNavigation).WithMany(p => p.MovimientoIdCuentaOrigenNavigations).HasConstraintName("FK__Movimient__id_cu__02FC7413");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("PK__TipoCuen__CF901089985D62EB");

            entity.Property(e => e.IdTipo).ValueGeneratedNever();
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Transacc__1EDAC099DF5677DE");

            entity.HasOne(d => d.IdCuentaDestinoNavigation).WithMany(p => p.TransaccionIdCuentaDestinoNavigations).HasConstraintName("FK__Transacci__id_cu__74AE54BC");

            entity.HasOne(d => d.IdCuentaOrigenNavigation).WithMany(p => p.TransaccionIdCuentaOrigenNavigations).HasConstraintName("FK__Transacci__id_cu__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
