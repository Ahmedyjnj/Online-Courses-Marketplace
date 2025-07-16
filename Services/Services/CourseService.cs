//using Abstraction;
//using AutoMapper;
//using Domain.Contracts;
//using Domain.Exceptions;
//using Domain.Models.Courses;
//using Services.Specifications;
//using Shared.Dto_s.CourseDto;
//using Shared.Dto_s.InstructorDto;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Services
//{
//    public class CourseService(IUnitOfWork unitOfWork,IMapper mapper): ICourseService
//    {

//        public async Task<IEnumerable<CourseReadDto>> GetAllAsync()
//        {
//            var spec = new CourseWithDetailsSpecification(null,false);

//            var courses =await unitOfWork.GetRepository<Course, Guid>()
//                .GetAllAsync(spec);

//            return mapper.Map<IEnumerable<CourseReadDto>>(courses);

//        }

//        public async Task<CourseReadDto> GetByIdAsync(Guid id)
//        {
//            var spec = new CourseWithDetailsSpecification(id);

//            var course =await unitOfWork.GetRepository<Course, Guid>()
//                .GetByIdAsync(spec);
                

//            if (course is null)
//                throw new CourseNotFoundException(id);

//            return mapper.Map<CourseReadDto>(course);
//        }



//        public async Task<IEnumerable<InstructorSimpleDto>> GetInstructorsByCourseId(Guid Id)
//        {
//            var spec = new CourseWithDetailsSpecification(Id, includeStudents: false, includeInstructors: true);
//            var course = await unitOfWork.GetRepository<Course, Guid>().GetByIdAsync(spec);

//            if (course is null)
//                throw new CourseNotFoundException(Id);

//            var instructors = course.InstructorCourses.Select(ic => ic.Instructor);

//            return mapper.Map<IEnumerable<InstructorSimpleDto>>(instructors);
//        }


//        public  async Task<bool> CreateAsync(CourseCreateDto dto)
//        {
//            var course = mapper.Map<Course>(dto);
//            unitOfWork.GetRepository<Course, Guid>().AddAsync(course);

//            return await unitOfWork.SaveChangesAsync()>0;

//        }

//        public async Task<bool> DeleteAsync(Guid id)
//        {
//            var repository = unitOfWork.GetRepository<Course, Guid>();
//            var course = repository.GetByIdAsync(id);
//            if (course is null)
//                throw new CourseNotFoundException(id);

//            repository.DeleteAsync(course.Result);

           
//            return await unitOfWork.SaveChangesAsync() > 0;

//        }

     
//        public async Task<bool> UpdateAsync(Guid id, CourseCreateDto dto)
//        {
//            var repository = unitOfWork.GetRepository<Course, Guid>();
//            var course =await repository.GetByIdAsync(id);
//            if (course is null)
//                return false;

//            mapper.Map(dto, course);


//            repository.UpdateAsync(course);


//            return await unitOfWork.SaveChangesAsync() > 0;

//        }
//    }
//}
