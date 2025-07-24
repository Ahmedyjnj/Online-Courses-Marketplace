using AutoMapper;
using Domain.Models.Students;
using Microsoft.Extensions.Configuration;
using Shared.Dto_s.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles.UrlResolver
{
    public class StudentResolver(IConfiguration configuration) : IValueResolver<Student, object, string>
    {
        public string Resolve(Student source, object destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PhotoUrl))
            {
                return string.Empty;

            }
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}/Content/Images/Students/{source.PhotoUrl}";
                return Url;
            }
        }
    }
}
