using AutoMapper;
using Domain.Models.Courses;
using Microsoft.Extensions.Configuration;
using Shared.Dto_s.ContentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles.UrlResolver
{
    public class ContentImageResolver(IConfiguration configuration) : IValueResolver<CourseImage, ImageDto, string>
    {

        public string Resolve(CourseImage source, ImageDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Url))
            {
                return string.Empty;

            }
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}/Content/Images/Courses/{source.Url}";
                return Url;
            }


        }
    }
}
