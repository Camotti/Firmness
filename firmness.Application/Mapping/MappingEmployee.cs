using AutoMapper;
using firmness.Domain.Entities;
using firmness.Application.DTOs;

namespace firmness.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Entity -> DTO
            CreateMap<Employee, EmployeeDto>();

            // DTO -> Entity
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}