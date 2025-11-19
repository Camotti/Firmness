using firmness.Application.DTOs;
using firmness.Domain.Entities;

namespace firmness.Application.Interfaces;

public interface ISalesService
{
    Task<List<SaleDto>> GetAllSalesAsync();
    Task<SaleDto?> GetSaleByIdAsync(int id);
    Task<bool> CreateSaleAsync(CreateSaleDto saleDto);
    Task<bool> UpdateSaleAsync(UpdateSaleDto saleDto);
    Task<bool> DeleteSaleAsync(int id);
    
    //listas formularios de ventas
    Task<List<ClientDto>> GetClientsAsync();
    Task<List<EmployeeDto>> GetEmployeesAsync();
    Task<List<ProductDto>> GetProductsAsync();
}
