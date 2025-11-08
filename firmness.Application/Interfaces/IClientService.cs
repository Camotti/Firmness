using firmness.Domain.Entities;
namespace firmness.Application.Interfaces
{
    public interface IClientService
    {
        Task<(bool Success, string Message)> CreateAsync(Client client);
        Task<List<Client>> GetAllAsync();
        Task<(bool Success, string Message)> UpdateAsync(Client client);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
