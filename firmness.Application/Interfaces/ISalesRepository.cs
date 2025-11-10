using firmness.Domain.Entities;
namespace firmness.Application.Interfaces;

public interface ISalesRepository
{
    Task<List<Sale>> GetAllAsync();
    Task<Sale> GetByIdAsync(int id);
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(int id);
    Task<List<Client>> GetClientsAsync();
    Task<List<Employee>> GetEmployeesAsync();
}