using firmness.Application.DTOs;
using firmness.Domain.Entities;
namespace firmness.Application.Interfaces
{
    public interface IClientService
    {
        Task<(bool Success, string Message)> CreateAsync(CreateClientDto clientDto);
        Task<List<ClientDto>> GetAllAsync();
        Task<(bool Success, string Message)> UpdateAsync(UpdateClientDto clientDto);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
