using AutoMapper;
using firmness.Application.DTOs;
using firmness.Domain.Entities;

namespace firmness.Application.Mapping;

public class MappingSale : Profile
{
    public MappingSale() 
    {
        CreateMap<Sale, SaleDto>();
        CreateMap<CreateSaleDto, Sale>();
        CreateMap<UpdateSaleDto, Sale>();
    }
}