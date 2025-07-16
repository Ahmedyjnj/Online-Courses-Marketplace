using AutoMapper;
using Domain.Models.Courses;
using Shared.Dto_s.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class CourseProfile : Profile
    {

        public CourseProfile()
        {
            CreateMap<Course, CourseReadDto>().ReverseMap();
                
           
            CreateMap<Course,CourseCreateDto>().ReverseMap();
        }

    }
}
