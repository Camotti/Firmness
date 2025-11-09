using firmness.Domain.Entities;
using firmness.Application.Interfaces;

namespace firmness.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<(bool Success, string Message)> CreateAsync(Client client)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(client.Name) || string.IsNullOrWhiteSpace(client.Document))
                    return (false, "El nombre y el documento son obligatorios.");

                // Validación de correo
                if (!client.Email.Contains("@"))
                    return (false, "El correo electrónico no es válido.");

                await _clientRepository.AddAsync(client);
                await _clientRepository.SaveAsync();

                return (true, "Cliente creado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear el cliente: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> UpdateAsync(Client client)
        {
            try
            {
                var existing = await _clientRepository.GetByIdAsync(client.Id);
                if (existing == null)
                    return (false, "Cliente no encontrado.");

                existing.Name = client.Name;
                existing.LastName = client.LastName;
                existing.Email = client.Email;
                existing.Phone = client.Phone;
                existing.Address = client.Address;
                existing.Document = client.Document;

                await _clientRepository.UpdateAsync(existing);
                await _clientRepository.SaveAsync();

                return (true, "Cliente actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el cliente: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            try
            {
                var existing = await _clientRepository.GetByIdAsync(id);
                if (existing == null)
                    return (false, "Cliente no encontrado.");

                await _clientRepository.DeleteAsync(id);
                await _clientRepository.SaveAsync();

                return (true, "Cliente eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el cliente: {ex.Message}");
            }
        }
    }
}