using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.ModelsTemp;

namespace WebApplication1.Data;

public partial class BurgiplotDB_MVPContext : DbContext
{
    public BurgiplotDB_MVPContext(DbContextOptions<BurgiplotDB_MVPContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente", tb => tb.HasTrigger("TRG_Cliente_ValidaNumericos"));

            entity.HasIndex(e => e.CUIT_CUIL, "UX_Cliente_CUIT_CUIL")
                .IsUnique()
                .HasFilter("([CUIT_CUIL] IS NOT NULL)");

            entity.Property(e => e.Apellido).HasMaxLength(200);
            entity.Property(e => e.CUIT_CUIL).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
