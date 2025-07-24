using AutoMapper;
using Domain.Models.Students;
using Services.MappingProfiles.UrlResolver;
using Shared.Dto_s.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class StudentProfile :Profile
    {
        public StudentProfile()
        {
            CreateMap<Student,StudentCreateDto>()

                .ReverseMap();

            CreateMap<Student, StudentReadDto>().ForMember(dest => dest.PhotoUrl, options => options.MapFrom<StudentResolver>())

                .ReverseMap();

            CreateMap<Student, StudentSimpleDto>()
                .ForMember(dest => dest.PhotoUrl, options => options.MapFrom<StudentResolver>())

                .ReverseMap();
        }
    }
}
