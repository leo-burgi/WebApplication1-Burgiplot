using Microsoft.EntityFrameworkCore; 

namespace WebApplication1.Models
{
    public class BurgiplotContext : DbContext
    {
        public BurgiplotContext(DbContextOptions<BurgiplotContext> options) : base(options) { }
        
        public DbSet<Cliente> Clientes { get; set; } //esto le indica a Entity Framework que la clase Cliente es una entidad que se mapea a una tabla en la base de datos.

    }

}
