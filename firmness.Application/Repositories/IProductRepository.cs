using firmness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace firmness.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}