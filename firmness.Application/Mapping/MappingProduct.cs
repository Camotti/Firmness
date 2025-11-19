using AutoMapper;
using firmness.Application.DTOs;
using firmness.Application.DTOs;
using firmness.Domain.Entities;

namespace firmness.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ProductDto>(); // Para retornar productos si quieres
        }
    }
}