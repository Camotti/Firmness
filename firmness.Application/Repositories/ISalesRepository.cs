using firmness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace firmness.Application.Interfaces.Repositories
{
    public interface ISalesRepository
    {
        Task<List<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(int id);
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(int id);
        Task<List<Client>> GetClientsAsync();
        Task<List<Employee>> GetEmployeesAsync();
    }
}