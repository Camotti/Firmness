using firmness.Data.Entities;

namespace firmness.Contracts
{
    public interface IProductService
    {
        Task<(bool Success, string Message)> CreateAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<(bool Success, string Message)> UpdateAsync(Product product);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
