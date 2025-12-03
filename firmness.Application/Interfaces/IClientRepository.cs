using firmness.Domain.Entities;

namespace firmness.Application.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(string id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(string id);
        Task SaveAsync();
    }
}

