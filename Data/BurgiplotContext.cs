using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public partial class BurgiplotContext : DbContext
{
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Correo, "UX_Cliente_Correo")
                .IsUnique()
                .HasFilter("([Correo] IS NOT NULL)");

            entity.HasIndex(e => e.CuitCuil, "UX_Cliente_CuitCuil")
                .IsUnique()
                .HasFilter("([CuitCuil] IS NOT NULL)");

            entity.Property(e => e.Correo).HasMaxLength(256);
            entity.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CuitCuil)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Dirección).HasMaxLength(200);
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

            entity.HasIndex(e => e.OrdenId, "IX_Factura_OrdenId").IsUnique();

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

            entity.HasOne(d => d.Orden).WithOne(p => p.Factura)
                .HasForeignKey<Factura>(d => d.OrdenId)
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

            entity.HasIndex(e => e.ClienteId, "IX_Orden_ClienteId");

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
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
