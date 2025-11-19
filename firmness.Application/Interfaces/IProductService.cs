using firmness.Application.DTOs;
using firmness.Domain.Entities;
namespace firmness.Application.Interfaces
{
    public interface IProductService
    {
        Task<(bool Success, string Message)> CreateAsync(CreateProductDto createProductDto);
        Task<List<ProductDto>> GetAllAsync();
        Task<(bool Success, string Message)> UpdateAsync(UpdateProductDto updateProductDto);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
