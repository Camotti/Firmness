using AutoMapper;
using firmness.Application.DTOs;
using firmness.Domain.Entities;

namespace firmness.Application.Mapping;

public class MappingClient : Profile
{
    public MappingClient()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<CreateClientDto, Client>();
        CreateMap<UpdateClientDto, Client>();
        CreateMap<ApplicationUser, ClientDto>()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
        CreateMap<CreateClientDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
            
        // Mapeo de UpdateClientDto a ApplicationUser
        CreateMap<UpdateClientDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
    }
}