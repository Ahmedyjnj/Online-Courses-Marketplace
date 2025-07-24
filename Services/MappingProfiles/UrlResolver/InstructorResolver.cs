using AutoMapper;
using Domain.Models.Instructors;
using Microsoft.Extensions.Configuration;
using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles.UrlResolver
{
    public class InstructorResolver(IConfiguration configuration) : IValueResolver<Instructor, object, string>
    {
        public string Resolve(Instructor source, object destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PhotoUrl))
            {
                return string.Empty;

            }
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}/Content/Images/Instructors/{source.PhotoUrl}";
                return Url;
            }
        }
    }
}
