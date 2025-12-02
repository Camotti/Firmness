using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using firmness.Application.DTOs;
using firmness.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using IClientRepository = firmness.Infrastructure.Repositories.IClientRepository;

namespace firmness.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ClientService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var clientUsers = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Client"))
                {
                    clientUsers.Add(user);
                }
            }

            // ⭐ Mapear directamente de ApplicationUser a ClientDto
            return _mapper.Map<List<ClientDto>>(clientUsers);
        }

        public async Task<(bool Success, string Message)> CreateAsync(CreateClientDto clientDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(clientDto.Name) || string.IsNullOrWhiteSpace(clientDto.Document))
                    return (false, "The name and the Document fields must not be empty.");

                if (!string.IsNullOrWhiteSpace(clientDto.Email) && !clientDto.Email.Contains("@"))
                    return (false, "El correo electrónico no es válido.");

                // Verificar si el usuario ya existe
                var existingUser = await _userManager.FindByEmailAsync(clientDto.Email);
                if (existingUser != null)
                    return (false, "Ya existe un usuario con este correo electrónico.");

                // Crear el ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = clientDto.Email,
                    Email = clientDto.Email,
                    Name = clientDto.Name,
                    LastName = clientDto.LastName,
                    PhoneNumber = clientDto.Phone,
                    Document = clientDto.Document,
                    Address = clientDto.Address
                };

                // Crear usuario con contraseña (necesitarás agregar Password a CreateClientDto)
                var result = await _userManager.CreateAsync(user, clientDto.Password ?? "DefaultPassword123!");
                
                if (!result.Succeeded)
                    return (false, $"Error al crear usuario: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                // Asignar rol de Cliente
                await _userManager.AddToRoleAsync(user, "Client");

                return (true, "Cliente creado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear el cliente: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> UpdateAsync(UpdateClientDto clientDto)
{
    try
    {
        var user = await _userManager.FindByIdAsync(clientDto.Id); // ⭐ Ya no necesitas .ToString()
        if (user == null)
            return (false, "Cliente no encontrado.");

        // Verificar que sea un cliente
        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Client"))
            return (false, "El usuario no es un cliente.");

        // Actualizar datos
        user.Name = clientDto.Name;
        user.LastName = clientDto.LastName;
        user.Email = clientDto.Email;
        user.UserName = clientDto.Email;
        user.PhoneNumber = clientDto.Phone;
        user.Document = clientDto.Document;
        user.Address = clientDto.Address;

        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded)
            return (false, $"Error al actualizar: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        return (true, "Cliente actualizado correctamente.");
    }
    catch (Exception ex)
    {
        return (false, $"Error al actualizar el cliente: {ex.Message}");
    }
}

        public async Task<(bool Success, string Message)> DeleteAsync(string id) // ⭐ Cambiar a string
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id); // ⭐ Ya no necesitas .ToString()
                if (user == null)
                    return (false, "Cliente no encontrado.");

                // Verificar que sea un cliente
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Client"))
                    return (false, "El usuario no es un cliente.");

                var result = await _userManager.DeleteAsync(user);
        
                if (!result.Succeeded)
                    return (false, $"Error al eliminar: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                return (true, "Cliente eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el cliente: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                    return (false, "Cliente no encontrado.");

                // Verificar que sea un cliente
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Client"))
                    return (false, "El usuario no es un cliente.");

                var result = await _userManager.DeleteAsync(user);
                
                if (!result.Succeeded)
                    return (false, $"Error al eliminar: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                return (true, "Cliente eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el cliente: {ex.Message}");
            }
        }
    }
}