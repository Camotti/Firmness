using firmness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace firmness.Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}