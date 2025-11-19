using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using firmness.Application.DTOs;
using firmness.Infrastructure.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace firmness.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public async Task<(bool Success, string Message)> CreateAsync(CreateClientDto clientDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(clientDto.Name) || string.IsNullOrWhiteSpace(clientDto.Document))
                    return (false, "El nombre y el documento son obligatorios.");

                if (!string.IsNullOrWhiteSpace(clientDto.Email) && !clientDto.Email.Contains("@"))
                    return (false, "El correo electrónico no es válido.");

                var client = _mapper.Map<Client>(clientDto);
                await _clientRepository.AddAsync(client);
                await _clientRepository.SaveAsync();

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
                var existing = await _clientRepository.GetByIdAsync(clientDto.Id);
                if (existing == null)
                    return (false, "Cliente no encontrado.");

                _mapper.Map(clientDto, existing);

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