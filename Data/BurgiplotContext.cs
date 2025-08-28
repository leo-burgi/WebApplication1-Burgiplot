using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class BurgiplotContext : DbContext
    {
        public BurgiplotContext(DbContextOptions<BurgiplotContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; } = null!;

    }

}
