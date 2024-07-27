using AutoMapper;
using EmployeeTaskManagement.Application.DTOs;
using EmployeeTaskManagement.Core.Entities;

namespace EmployeeTaskManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeTask, TaskDto>()
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents)).ReverseMap();

            CreateMap<EmployeeDocument, DocumentDto>().ReverseMap();
        }
    }
}
