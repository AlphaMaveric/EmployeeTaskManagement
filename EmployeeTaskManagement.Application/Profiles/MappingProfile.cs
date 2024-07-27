using AutoMapper;
using EmployeeTaskManagement.Application.DTOs;
using EmployeeTaskManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
