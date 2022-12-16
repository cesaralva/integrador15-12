using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiCondominio.Models;

public partial class CondominioContext : DbContext
{
    public CondominioContext()
    {
    }

    public CondominioContext(DbContextOptions<CondominioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<Incidente> Incidentes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Recibo> Recibos { get; set; }

    public virtual DbSet<Recibosdetalle> Recibosdetalles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Condominio");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3213E83F1CFFB09B");

            entity.HasIndex(e => e.Descripcion, "UQ__Departam__298336B6551FB1FD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fechaactualiza)
                .HasColumnType("datetime")
                .HasColumnName("fechaactualiza");
            entity.Property(e => e.Fecharegistra)
                .HasColumnType("datetime")
                .HasColumnName("fecharegistra");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Piso).HasColumnName("piso");
            entity.Property(e => e.Torre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("torre");
            entity.Property(e => e.Usuarioactualiza).HasColumnName("usuarioactualiza");
            entity.Property(e => e.Usuarioregistra).HasColumnName("usuarioregistra");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Obligacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Concepto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("concepto");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Monto)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.Tipoobligacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipoobligacion");
        });

        modelBuilder.Entity<Incidente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Incident__3213E83FA3FF9D43");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Detalle)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.Documento).HasColumnName("documento");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Fechaactualiza)
                .HasColumnType("datetime")
                .HasColumnName("fechaactualiza");
            entity.Property(e => e.Fecharegistra)
                .HasColumnType("datetime")
                .HasColumnName("fecharegistra");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Observacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("observacion");
            entity.Property(e => e.Usuarioactualiza).HasColumnName("usuarioactualiza");
            entity.Property(e => e.Usuarioregistra).HasColumnName("usuarioregistra");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Incidentes)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("FK__Incidente__idusu__35BCFE0A");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3213E83F26FC844F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fechapago)
                .HasColumnType("datetime")
                .HasColumnName("fechapago");
            entity.Property(e => e.Idrecibo).HasColumnName("idrecibo");
            entity.Property(e => e.Ruta)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdreciboNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.Idrecibo)
                .HasConstraintName("FK_Pagos_Recibos");
        });

        modelBuilder.Entity<Recibo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Liquidaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estadopago)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("estadopago");
            entity.Property(e => e.Fechavencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fechavencimiento");
            entity.Property(e => e.Iddepartamento).HasColumnName("iddepartamento");
            entity.Property(e => e.Mes)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("mes");
            entity.Property(e => e.Montopago)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("montopago");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.IddepartamentoNavigation).WithMany(p => p.Recibos)
                .HasForeignKey(d => d.Iddepartamento)
                .HasConstraintName("FK_Recibos_Departamentos");
        });

        modelBuilder.Entity<Recibosdetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Obligaliquidaciones");

            entity.ToTable("Recibosdetalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idgastos).HasColumnName("idgastos");
            entity.Property(e => e.Idrecibo).HasColumnName("idrecibo");
            entity.Property(e => e.Monto)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.IdgastosNavigation).WithMany(p => p.Recibosdetalles)
                .HasForeignKey(d => d.Idgastos)
                .HasConstraintName("FK_Obligaliquidaciones_Obligaciones1");

            entity.HasOne(d => d.IdreciboNavigation).WithMany(p => p.Recibosdetalles)
                .HasForeignKey(d => d.Idrecibo)
                .HasConstraintName("FK_Obligaliquidaciones_Liquidaciones");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F5D4AEE66");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcionrol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcionrol");
            entity.Property(e => e.Fechaactualiza)
                .HasColumnType("datetime")
                .HasColumnName("fechaactualiza");
            entity.Property(e => e.Fecharegistra)
                .HasColumnType("datetime")
                .HasColumnName("fecharegistra");
            entity.Property(e => e.Usuarioactualiza).HasColumnName("usuarioactualiza");
            entity.Property(e => e.Usuarioregistra).HasColumnName("usuarioregistra");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83F57E75B49");

            entity.HasIndex(e => e.Telefono, "UQ__Usuarios__2A16D94507A5F902").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuarios__AB6E6164915A3C45").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.CodigoVerificacion).HasColumnName("codigoVerificacion");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaVerificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaVerificacion");
            entity.Property(e => e.Fechaactualiza)
                .HasColumnType("datetime")
                .HasColumnName("fechaactualiza");
            entity.Property(e => e.Fecharegistra)
                .HasColumnType("datetime")
                .HasColumnName("fecharegistra");
            entity.Property(e => e.Iddepartamento).HasColumnName("iddepartamento");
            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Nombres)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
            entity.Property(e => e.Usuarioactualiza).HasColumnName("usuarioactualiza");
            entity.Property(e => e.Usuarioregistra).HasColumnName("usuarioregistra");

            entity.HasOne(d => d.IddepartamentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Iddepartamento)
                .HasConstraintName("FK__Usuarios__iddepa__3A81B327");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK__Usuarios__idrol__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
