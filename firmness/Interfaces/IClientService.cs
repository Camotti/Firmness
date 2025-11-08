using firmness.Data.Entities;

namespace firmness.Interfaces
{
    public interface IClientService
    {
        Task<(bool Success, string Message)> CreateAsync(Client client);
        Task<List<Client>> GetAllAsync();
        Task<(bool Success, string Message)> UpdateAsync(Client client);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
