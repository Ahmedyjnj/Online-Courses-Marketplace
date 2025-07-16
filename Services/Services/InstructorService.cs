//using Abstraction;
//using AutoMapper;
//using Domain.Contracts;
//using Domain.Exceptions;
//using Domain.Models.Courses;
//using Domain.Models.Instructors;
//using Shared.Dto_s.InstructorDto;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Services
//{
//    public class InstructorService(IUnitOfWork unitOfWork,IMapper mapper) : IInstructorService
//    {

//        public async Task<InstructorReadDto> GetByIdAsync(Guid id)
//        {
//            var instructor = await unitOfWork.GetRepository<Instructor, Guid>()
//                .GetByIdAsync(id);
//            if (instructor is null)
//                throw new InstructorNotFoundException(id);

//            return mapper.Map<InstructorReadDto>(instructor);
//        }

//        public async Task<IEnumerable<InstructorReadDto>> GetAllAsync()
//        {
//            var instructors = await unitOfWork.GetRepository<Instructor, Guid>()
//                .GetAllAsync();
//            return mapper.Map<IEnumerable<InstructorReadDto>>(instructors);
//        }

      

//        public async Task<bool> AssignInstructorToCourse(Guid instructorId, Guid courseId)
//        {
//            var instructorRepo = unitOfWork.GetRepository<Instructor, Guid>();
//            var courseRepo = unitOfWork.GetRepository<Course, Guid>();

//            var instructor = await instructorRepo.GetByIdAsync(instructorId)
//                ?? throw new InstructorNotFoundException(instructorId);

//            var course = await courseRepo.GetByIdAsync(courseId)
//                ?? throw new CourseNotFoundException(courseId);

//            // افترض إن InstructorCourse عنده Id جديد (Guid)
//            var instructorCourse = new InstructorCourse
//            {
                
//                InstructorId = instructorId,
//                CourseId = courseId
//            };

//            var icRepo = unitOfWork.GetRepositoryWithNoid<InstructorCourse>();

//            icRepo.AddAsync(instructorCourse);

//            return await unitOfWork.SaveChangesAsync()>0; 
//        }

//        public async Task<bool> CreateAsync(InstructorCreateDto dto)
//        {
//            var instructor = mapper.Map<Instructor>(dto);

//            var repository = unitOfWork.GetRepository<Instructor, Guid>();
//            repository.AddAsync(instructor);

//            return await unitOfWork.SaveChangesAsync() > 0;

//        }

//        public async Task<bool> DeleteAsync(Guid id)
//        {
//            var repository = unitOfWork.GetRepository<Instructor, Guid>();
//            var instructor = await repository.GetByIdAsync(id);

//            if (instructor is null)
//                throw new InstructorNotFoundException(id);

            
//             repository.DeleteAsync(instructor);
//            return await unitOfWork.SaveChangesAsync() > 0;
//        }

       

//        public async Task<bool> UpdateAsync(Guid id, InstructorCreateDto dto)
//        {
//            var repository = unitOfWork.GetRepository<Instructor, Guid>();
//            var existingInstructor = await repository.GetByIdAsync(id);

//            if (existingInstructor is null)
//                throw new InstructorNotFoundException(id);

//            mapper.Map(dto, existingInstructor);


//            repository.UpdateAsync(existingInstructor);
//            return await unitOfWork.SaveChangesAsync() > 0;
//        }
//    }
//}
