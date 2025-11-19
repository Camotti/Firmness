using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using firmness.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using firmness.Application.DTOs;

namespace firmness.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<(bool Success, string Message)> CreateAsync(CreateProductDto createProductDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createProductDto.Name))
                    return (false, "El nombre del producto es obligatorio.");

                if (createProductDto.Price < 0)
                    return (false, "El precio no puede ser negativo.");

                if (createProductDto.Stock < 0)
                    return (false, "El stock no puede ser negativo.");

                // mapeamos el DTO a la entidad product
                var product = _mapper.Map<Product>(createProductDto);
                
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

        public async Task<(bool Success, string Message)> UpdateAsync(UpdateProductDto updateProductDto)
        {
            try
            {
                if (updateProductDto.Id <= 0)
                    return (false, "ID de producto inválido.");

                var product = _mapper.Map<Product>(updateProductDto);
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
