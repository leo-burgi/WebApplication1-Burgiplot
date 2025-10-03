using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public partial class BurgiplotContext : DbContext
    {
        public BurgiplotContext(DbContextOptions<BurgiplotContext> options) : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entity = modelBuilder.Entity<Cliente>();

            
            entity.ToTable("Cliente", tb => tb.HasTrigger("TRG_Cliente_ValidaNumericos"));
            entity.Property(e => e.Apellido).HasMaxLength(200);
            entity.Property(e => e.CUIT_CUIL).HasMaxLength(20);

            entity.HasIndex(e => e.CUIT_CUIL, "UX_Cliente_CUIT_CUIL")
                    .IsUnique()
                    .HasFilter("([CUIT_CUIL] IS NOT NULL)");

                
            

            //OnModelCreating(modelBuilder);
        }
    }

}
