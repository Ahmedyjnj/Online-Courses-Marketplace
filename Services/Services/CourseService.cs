using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Courses;
using Domain.Models.Instructors;
using Domain.Models.Students;
using Services.Specifications;
using Shared.Dto_s.CourseDto;
using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CourseService(IUnitOfWork unitOfWork, IMapper mapper) : ICourseService
    {

        public async Task<IEnumerable<CourseReadDto>> GetAllAsync(string? search )
        {
           
            if (search != null)
            {
                search=search.Trim().ToLower();
            }

            var spec = new CourseWithDetailsSpecification( name:search , includeInstructors: true);


          

            var courses = await unitOfWork.GetRepository<Course, Guid>()
                .GetAllAsync(spec);


            return mapper.Map<IEnumerable<CourseReadDto>>(courses);

        }

        public async Task<CourseReadDto> GetByIdAsync(Guid id)
        {
           

            var course = await unitOfWork.GetRepository<Course, Guid>()
                .GetByIdAsync(id);


            if (course is null)
                throw new CourseNotFoundException(id);

            return mapper.Map<CourseReadDto>(course);
        }

    
        public async Task<IEnumerable<CourseReadDto>> GetCourseswithInstructorId(Guid Id)
        {
            var spec = new CourseWithDetailsSpecification(InstructorId:Id);

            var courses =await unitOfWork.GetRepository<Course, Guid>()
                .GetAllAsync(spec);

           
            if (courses is null || !courses.Any())
                return Enumerable.Empty<CourseReadDto>();


            return mapper.Map<IEnumerable<CourseReadDto>>(courses);

        }


        public async Task<bool> CreateAsync(CourseCreateDto dto)
        {
            var course = mapper.Map<Course>(dto);
            unitOfWork.GetRepository<Course, Guid>().AddAsync(course);

            return await unitOfWork.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var repository = unitOfWork.GetRepository<Course, Guid>();
            var course = repository.GetByIdAsync(id);
            if (course is null)
                throw new CourseNotFoundException(id);

            repository.DeleteAsync(course.Result);


            return await unitOfWork.SaveChangesAsync() > 0;

        }


        public async Task<bool> UpdateAsync(Guid id, CourseCreateDto dto)
        {
            var repository = unitOfWork.GetRepository<Course, Guid>();
            var course = await repository.GetByIdAsync(id);
            if (course is null)
                return false;

            mapper.Map(dto, course);


            repository.UpdateAsync(course);


            return await unitOfWork.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<CourseReadDto>> GetByStudentIdAsync(Guid studentId)
        {
            var enrollments = await unitOfWork.GetRepositoryWithNoid<StudentEnrollment>()
                .GetWhereAsync(e => e.StudentId == studentId);

          

            if (!enrollments.Any())
                return new List<CourseReadDto>();

            var courseIds = enrollments.Select(e => e.CourseId).ToList();

            var courses = await unitOfWork.GetRepository<Course, Guid>()
                .GetAllAsync();

            var studentCourses = courses.Where(c => courseIds.Contains(c.Id));

            return mapper.Map<List<CourseReadDto>>(studentCourses);
        }


    }
}
