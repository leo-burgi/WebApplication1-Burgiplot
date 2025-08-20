using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public partial class BurgiplotContext : DbContext
{
    public BurgiplotContext()
    {
    }

    public BurgiplotContext(DbContextOptions<BurgiplotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaItem> FacturaItems { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Orden> Ordens { get; set; }

    public virtual DbSet<OrdenItem> OrdenItems { get; set; }

    public virtual DbSet<StockMovimiento> StockMovimientos { get; set; }

    public virtual DbSet<VwFacturacionAnual> VwFacturacionAnuals { get; set; }

    public virtual DbSet<VwFacturacionMensual> VwFacturacionMensuals { get; set; }

    public virtual DbSet<VwOrdenesMensuale> VwOrdenesMensuales { get; set; }

    public virtual DbSet<VwStockSaldo> VwStockSaldos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6UDLR1E\\SQLEXPRESS;Database=BurgiplotDB;Encrypt=True;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Correo, "UX_Cliente_Correo")
                .IsUnique()
                .HasFilter("([Correo] IS NOT NULL)");

            entity.Property(e => e.Correo).HasMaxLength(256);
            entity.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Dirección).HasMaxLength(200);
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .HasColumnName("DNI");
            entity.Property(e => e.Nombre).HasMaxLength(120);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.UpdateAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Factura__3214EC0789DB5EA9");

            entity.ToTable("Factura");

            entity.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FechaUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Numero).HasMaxLength(30);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Orden).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__OrdenId__73BA3083");
        });

        modelBuilder.Entity<FacturaItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FacturaI__3214EC079EC1FFB0");

            entity.ToTable("FacturaItem");

            entity.HasIndex(e => e.FacturaId, "IX_FacturaItem_FacturaId");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.PrecioUnit).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Factura).WithMany(p => p.FacturaItems)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__FacturaIt__Factu__787EE5A0");

            entity.HasOne(d => d.Material).WithMany(p => p.FacturaItems)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FacturaIt__Mater__797309D9");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC07760696B6");

            entity.ToTable("Material");

            entity.HasIndex(e => e.Codigo, "UX_Material_Codigo").IsUnique();

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.PrecioStd).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Unidad).HasMaxLength(20);
            entity.Property(e => e.UpdatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orden__3214EC0799DB4AB4");

            entity.ToTable("Orden");

            entity.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Borrador");
            entity.Property(e => e.FechaUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.RowVer)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.UpdatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orden__ClienteId__693CA210");
        });

        modelBuilder.Entity<OrdenItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenIte__3214EC075B99406A");

            entity.ToTable("OrdenItem");

            entity.HasIndex(e => e.OrdenId, "IX_OrdenItem_OrdenId");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.PrecioUnit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnit])", true)
                .HasColumnType("decimal(37, 5)");

            entity.HasOne(d => d.Material).WithMany(p => p.OrdenItems)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenItem__Mater__70DDC3D8");

            entity.HasOne(d => d.Orden).WithMany(p => p.OrdenItems)
                .HasForeignKey(d => d.OrdenId)
                .HasConstraintName("FK__OrdenItem__Orden__6FE99F9F");
        });

        modelBuilder.Entity<StockMovimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockMov__3214EC07DDCBCEF4");

            entity.ToTable("StockMovimiento", tb => tb.HasTrigger("TR_StockMovimiento_NoNegativo"));

            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.FechaUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Motivo).HasMaxLength(60);
            entity.Property(e => e.RefTipo).HasMaxLength(30);

            entity.HasOne(d => d.Material).WithMany(p => p.StockMovimientos)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StockMovi__Mater__6477ECF3");
        });

        modelBuilder.Entity<VwFacturacionAnual>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FacturacionAnual");

            entity.Property(e => e.ImporteAño).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwFacturacionMensual>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FacturacionMensual");

            entity.Property(e => e.ImporteMes).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwOrdenesMensuale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_OrdenesMensuales");
        });

        modelBuilder.Entity<VwStockSaldo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_StockSaldo");

            entity.Property(e => e.Saldo).HasColumnType("decimal(38, 3)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
