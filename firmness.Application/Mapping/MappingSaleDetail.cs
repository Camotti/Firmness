using AutoMapper;
using firmness.Application.DTOs;
using firmness.Domain.Entities;

namespace firmness.Application.Mapping;

public class MappingSaleDetail : Profile
{
    public  MappingSaleDetail() // constructor 
    {
        CreateMap<SaleDetail, SaleDetailDto>();
        CreateMap<CreateSaleDetailDto, SaleDetail>();
        CreateMap<UpdateSaleDetailDto, SaleDetail>();
    }
}