using AutoMapper;
using Domain.Models.Courses;
using Domain.Models.Instructors;
using Microsoft.Extensions.Configuration;
using Services.MappingProfiles.UrlResolver;
using Shared.Dto_s.CourseDto;
using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class InstructorProfile :Profile
    {
        public InstructorProfile()
        {
            CreateMap<Instructor, InstructorCreateDto>()
               .ReverseMap();



            CreateMap<Instructor, InstructorReadDto>()
                .ForMember(Dist => Dist.PhotoUrl, options => options.MapFrom<InstructorResolver>())


                .ReverseMap();

            CreateMap<Instructor, InstructorSimpleDto>()
                 .ForMember(Dist => Dist.PhotoUrl, options => options.MapFrom<InstructorResolver>())
                .ReverseMap();
        }
    }
}
