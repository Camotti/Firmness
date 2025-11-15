using firmness.Domain.Entities;

namespace firmness.Application.Interfaces;

public interface ISalesService
{
    Task<List<Sale>> GetAllSalesAsync();
    Task<Sale> GetSaleByIdAsync(int id);
    Task<bool> CreateSaleAsync(Sale sale);
    Task<bool> UpdateSaleAsync(Sale sale);
    Task<bool> DeleteSaleAsync(int id);
    
    //listas formularios de ventas
    Task<List<Client>> GetClientsAsync();
    Task<List<Employee>> GetEmployeesAsync();
    Task<List<Product>> GetProductsAsync();
}