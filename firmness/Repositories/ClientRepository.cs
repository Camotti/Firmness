using firmness.Data;
using firmness.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace firmness.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los clientes
        public async Task<List<Client>> GetAllAsync() =>
            await _context.Clients.ToListAsync();

        // Obtener cliente por ID
        public async Task<Client?> GetByIdAsync(int id) =>
            await _context.Clients.FindAsync(id);

        // Agregar nuevo cliente
        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        // Actualizar cliente existente
        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await Task.CompletedTask;
        }

        // Eliminar cliente por ID
        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
        }

        // Guardar cambios en la base de datos
        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
