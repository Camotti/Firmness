using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using firmness.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace firmness.Application.Services
{
    public class SalesService
    {
        private readonly SalesRepository _salesRepo;

        public SalesService(SalesRepository salesRepo)
        {
            _salesRepo = salesRepo;
        }

        public async Task<List<Sale>> GetAllSalesAsync() => await _salesRepo.GetAllAsync();

        public async Task<Sale?> GetSaleByIdAsync(int id) => await _salesRepo.GetByIdAsync(id);

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

        public async Task<List<Client>> GetClientsAsync() => await _salesRepo.GetClientsAsync();

        public async Task<List<Employee>> GetEmployeesAsync() => await _salesRepo.GetEmployeesAsync();
    }
}