using firmness.Application.Interfaces;

namespace firmness.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<(bool Success, string Message)> CreateAsync(Product product)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.Name))
                    return (false, "El nombre del producto es obligatorio.");

                if (product.Price < 0)
                    return (false, "El precio no puede ser negativo.");

                if (product.Stock < 0)
                    return (false, "El stock no puede ser negativo.");

                await _repo.AddAsync(product);
                await _repo.SaveAsync();

                return (true, "Producto creado correctamente.");
            }
            catch (FormatException ex)
            {
                return (false, $"Error de formato: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<(bool Success, string Message)> UpdateAsync(Product product)
        {
            try
            {
                if (product.Id <= 0)
                    return (false, "ID de producto inválido.");

                await _repo.UpdateAsync(product);
                await _repo.SaveAsync();

                return (true, "Producto actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            try
            {
                await _repo.DeleteAsync(id);
                await _repo.SaveAsync();

                return (true, "Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar: {ex.Message}");
            }
        }
    }
}
