using AutoMapper;
using Domain.Models.Courses;
using Domain.Models.Instructors;
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
            CreateMap<Instructor, InstructorCreateDto>().ReverseMap();



            CreateMap<Instructor, InstructorReadDto>().ReverseMap();


            CreateMap<Instructor, InstructorSimpleDto>().ReverseMap();
        }
    }
}
