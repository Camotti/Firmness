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
    }
}