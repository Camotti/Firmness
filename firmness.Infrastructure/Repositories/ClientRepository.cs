using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace firmness.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Obtener todos los clientes (usuarios con rol "Client")
        public async Task<List<Client>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var clients = new List<Client>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Client"))
                {
                    clients.Add(new Client
                    {
                        Id = user.Id,
                        Name = user.Name,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone = user.PhoneNumber,
                        Document = user.Document,
                        Address = user.Address
                    });
                }
            }

            return clients;
        }

        // Obtener cliente por ID
        public async Task<Client?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Client"))
                return null;

            return new Client
            {
                Id = id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Document = user.Document,
                Address = user.Address
            };
        }

        // Agregar nuevo cliente
        public async Task AddAsync(Client client)
        {
            var user = new ApplicationUser
            {
                UserName = client.Email,
                Email = client.Email,
                Name = client.Name,
                LastName = client.LastName,
                PhoneNumber = client.Phone,
                Document = client.Document,
                Address = client.Address
            };

            // Nota: Necesitarás pasar la contraseña desde el servicio
            // Este método ya no es suficiente, ver cambios en el servicio
        }

        // Actualizar cliente existente
        public async Task UpdateAsync(Client client)
        {
            var user = await _userManager.FindByIdAsync(client.Id.ToString());
            if (user != null)
            {
                user.Name = client.Name;
                user.LastName = client.LastName;
                user.Email = client.Email;
                user.UserName = client.Email;
                user.PhoneNumber = client.Phone;
                user.Document = client.Document;
                user.Address = client.Address;

                await _userManager.UpdateAsync(user);
            }
        }

        // Eliminar cliente por ID
        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        // Guardar cambios (no necesario con UserManager, pero se mantiene por interfaz)
        public async Task SaveAsync()
        {
            await Task.CompletedTask;
        }
    }
}