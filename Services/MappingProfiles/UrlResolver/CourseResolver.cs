using AutoMapper;
using Domain.Models.Courses;
using Domain.Models.Instructors;
using Microsoft.Extensions.Configuration;
using Shared.Dto_s.CourseDto;
using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles.UrlResolver
{
    public class CourseResolver(IConfiguration configuration) : IValueResolver<Course, CourseReadDto, string>
    {
        public string Resolve(Course source, CourseReadDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PhotoUrl))
            {
                return string.Empty;

            }
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}/Content/Images/Courses/{source.PhotoUrl}";
                return Url;
            }
        }
    }
}
