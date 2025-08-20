using Microsoft.EntityFrameworkCore; 

namespace WebApplication1.Models
{
    public class BurgiplotContext : DbContext
    {
        public BurgiplotContext(DbContextOptions<BurgiplotContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; } = null!;

    }

}
