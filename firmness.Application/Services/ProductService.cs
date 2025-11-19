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

        public async Task<(bool Success, string Message)> CreateAsync(CreateProductDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return (false, "El nombre del producto es obligatorio.");

                if (dto.Price < 0)
                    return (false, "El precio no puede ser negativo.");

                if (dto.Stock < 0)
                    return (false, "El stock no puede ser negativo.");

                var product = _mapper.Map<Product>(dto);

                await _repo.AddAsync(product);
                await _repo.SaveAsync();

                return (true, "Producto creado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<(bool Success, string Message)> UpdateAsync(UpdateProductDto dto)
        {
            try
            {
                if (dto.Id <= 0)
                    return (false, "ID de producto inválido.");

                var existing = await _repo.GetByIdAsync(dto.Id);
                if (existing == null)
                    return (false, "Producto no encontrado.");

                // Mapear SOLO los campos que vienen del DTO
                _mapper.Map(dto, existing);

                await _repo.UpdateAsync(existing);
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
                var existing = await _repo.GetByIdAsync(id);
                if (existing == null)
                    return (false, "Producto no encontrado.");

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