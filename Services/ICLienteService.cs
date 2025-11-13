using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IClienteService
    //interface que define los metodos para manejar las operaciones CRUD de los clientes
    {
        Task<List<Cliente>> GetAllClientesAsync(string? searchString);
        Task<Cliente?> GetClienteByIdAsync(int id);
        Task CreateClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);
    }
}
