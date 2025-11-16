using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using firmness.Application.Interfaces.Repositories;
using firmness.Infrastructure.Repositories;

namespace firmness.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepo;

        public SalesService(ISalesRepository salesRepo)
        {
            _salesRepo = salesRepo;
        }

        public async Task<List<Sale>> GetAllSalesAsync()
        {
            var sales = await _salesRepo.GetAllAsync();
            return sales.ToList();
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)  // 
        {
            return await _salesRepo.GetByIdAsync(id);
        }

        public async Task<bool> CreateSaleAsync(Sale sale)
        {
            try
            {
                await _salesRepo.AddAsync(sale);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateSaleAsync(Sale sale)
        {
            try
            {
                await _salesRepo.UpdateAsync(sale);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            try
            {
                await _salesRepo.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Client>> GetClientsAsync() => 
            await _salesRepo.GetClientsAsync();

        public async Task<List<Employee>> GetEmployeesAsync() => 
            await _salesRepo.GetEmployeesAsync();

        
        public async Task<List<Product>> GetProductsAsync() => 
            await _salesRepo.GetProductsAsync();
    }
}