using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Courses;
using Domain.Models.Instructors;
using Domain.Models.Students;
using Microsoft.AspNetCore.Http;
using Shared.Dto_s.ContentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Identity.ApplicationUser;

namespace Services.Services
{
    public class ContentServices(IUnitOfWork unitOfWork, IMapper mapper) : IContentServices
    {

        public async Task<IEnumerable<ContentDto>> GetAllAsync(Guid courseid)
        {
            var videos = await unitOfWork
             .GetRepositoryWithNoid<CourseVideo>()
             .GetWhereAsync(v => v.CourseId == courseid);

            var images = await unitOfWork
                .GetRepositoryWithNoid<CourseImage>()
                .GetWhereAsync(i => i.CourseId == courseid);

            var videoDtos = mapper.Map<IEnumerable<VideoDto>>(videos);
            var imageDtos = mapper.Map<IEnumerable<ImageDto>>(images);


            var imageMap = imageDtos.ToDictionary(i => i.Id, i => i);

            var contentList = videoDtos
                .Select(video => new ContentDto
                {
                    VideoDto = video,
                    ImageDto = imageMap.TryGetValue(video.Id, out var image) ? image : null,

                })
                .ToList();



            return contentList;
        }
        public async Task<IEnumerable<ContentDto>> GetAllWithoutAccerssAsync(Guid courseid)
        {
            var videos = await unitOfWork
             .GetRepositoryWithNoid<CourseVideo>()
             .GetWhereAsync(v => v.CourseId == courseid);

            var images = await unitOfWork
                .GetRepositoryWithNoid<CourseImage>()
                .GetWhereAsync(i => i.CourseId == courseid);

            var videoDtos = mapper.Map<IEnumerable<VideoDto>>(videos);
            var imageDtos = mapper.Map<IEnumerable<ImageDto>>(images);

            foreach (var video in videoDtos)
            {
                video.Url = null;
            }

            var imageMap = imageDtos.ToDictionary(i => i.Id, i => i);

            var contentList = videoDtos
                .Select(video => new ContentDto
                {
                    VideoDto = video,
                    ImageDto = imageMap.TryGetValue(video.Id, out var image) ? image : null,

                })
                .ToList();



            return contentList;
        }
        public async Task<ContentDto> GetByIdAsync(Guid contentId)
        {
            var videoList = await unitOfWork
                .GetRepositoryWithNoid<CourseVideo>()
                .GetWhereAsync(v => v.Id == contentId);

            var video = videoList.FirstOrDefault();

            // ثم استعلام الصورة بعد انتهاء الأول
            var imageList = await unitOfWork
                .GetRepositoryWithNoid<CourseImage>()
                .GetWhereAsync(i => i.Id == contentId);

            var image = imageList.FirstOrDefault();

            // لا يوجد فيديو أو صورة
            if (video is null && image is null)
                return null;

            // بناء الكائن النهائي
            return new ContentDto
            {
                CourseId = video?.CourseId ?? image?.CourseId ?? Guid.Empty,
                VideoDto = video != null ? mapper.Map<VideoDto>(video) : null,
                ImageDto = image != null ? mapper.Map<ImageDto>(image) : null
            };
        }







        public async Task<bool> UpdateContent(ContentDto content)
        {
            if (content is null || content.VideoDto is null)
                return false;

            var contentId = content.VideoDto.Id;

            var videoRepo = unitOfWork.GetRepositoryWithNoid<CourseVideo>();
            var imageRepo = unitOfWork.GetRepositoryWithNoid<CourseImage>();

            var existingVideo = await videoRepo.GetWhereAsync(i => i.Id == contentId);
            var existingImage = await imageRepo.GetWhereAsync(i => i.Id == contentId);

            if (existingVideo == null && existingImage == null)
                return false;

          
            mapper.Map(content.VideoDto, existingVideo.FirstOrDefault());

            existingVideo.FirstOrDefault().CourseId = (Guid)content.CourseId;

            videoRepo.UpdateAsync(existingVideo.FirstOrDefault());

          
            if (content.ImageDto != null && existingImage != null)
            {
                mapper.Map(content.ImageDto, existingImage.FirstOrDefault());
                existingImage.FirstOrDefault().CourseId = (Guid)content.CourseId;
                imageRepo.UpdateAsync(existingImage.FirstOrDefault());
            }

            await unitOfWork.SaveChangesAsync();

            return true;
        }


        public async Task<bool> AddContent(ContentDto content)
        {
            //we should get courseid from dto


            var CourseId = content.CourseId;


            var video = mapper.Map<CourseVideo>(content.VideoDto);
            video.CourseId = (Guid)CourseId;

            var image = content.ImageDto != null ? mapper.Map<CourseImage>(content.ImageDto) : null;
            image.CourseId = (Guid)CourseId;

            unitOfWork.GetRepositoryWithNoid<CourseVideo>().AddAsync(video);

            if (image != null)
            {
                unitOfWork.GetRepositoryWithNoid<CourseImage>().AddAsync(image);
            }

            await unitOfWork.SaveChangesAsync();
            return true;
        }


      

        public async Task<bool> DeleteContent(Guid contentid)
        {
            var videoRepo = unitOfWork.GetRepositoryWithNoid<CourseVideo>();
            var imageRepo = unitOfWork.GetRepositoryWithNoid<CourseImage>();

            var existingVideo = await videoRepo.GetWhereAsync(i => i.Id == contentid);
            var existingImage = await imageRepo.GetWhereAsync(i => i.Id == contentid);

            if (existingVideo?.FirstOrDefault() != null)
            {
                await videoRepo.DeleteAsync(existingVideo.FirstOrDefault());
            }

            if (existingImage?.FirstOrDefault() != null)
            {
                await imageRepo.DeleteAsync(existingImage.FirstOrDefault());
            }

            await unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<bool> AccessState(Guid CourseId, Guid CurrentUser, string userType)
        {

            if (userType == "Student")
            {
                var studentRepo = unitOfWork.GetRepository<Student, Guid>();
                var courseRepo = unitOfWork.GetRepository<Course, Guid>();
                var enrollmentRepo = unitOfWork.GetRepositoryWithNoid<StudentEnrollment>();

                var student = await studentRepo.GetByIdAsync(CurrentUser)
                    ?? throw new StudentNotFoundException(CurrentUser);

                var course = await courseRepo.GetByIdAsync(CourseId)
                    ?? throw new CourseNotFoundException(CourseId);

                var enrollment = await enrollmentRepo.GetWhereAsync(e => e.CourseId == CourseId && e.StudentId == CurrentUser);

                if (enrollment != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (userType == "Instructor")
            {

                var instructor = await unitOfWork.GetRepository<Instructor, Guid>()
               .GetByIdAsync(CurrentUser);

                var course = await unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(CourseId);

                if (course.InstructorId == instructor.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            return false;
        }

    }
}
