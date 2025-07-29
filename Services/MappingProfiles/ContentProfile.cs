using AutoMapper;
using Domain.Models.Courses;
using Services.MappingProfiles.UrlResolver;
using Shared.Dto_s.ContentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
   public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            CreateMap<CourseVideo, VideoDto>();
                
            CreateMap<VideoDto, CourseVideo>();


            CreateMap<CourseImage, ImageDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom<ContentImageResolver>());

            CreateMap<ImageDto,CourseImage>();
        }
    }
}
