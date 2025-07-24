using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Courses;
using Domain.Models.Students;
using Services.Specifications;
using Shared.Dto_s.EnrollmentDto;
using Shared.Dto_s.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class StudentServices(IUnitOfWork unitOfWork ,IMapper mapper) : IStudentService
    {
        public async Task<IEnumerable<StudentReadDto>> GetAllAsync()
        {
            var students = await unitOfWork.GetRepository<Student, Guid>()
               .GetAllAsync();

            return mapper.Map<IEnumerable<StudentReadDto>>(students);
        }

        public async Task<StudentReadDto> GetByIdAsync(Guid id)
        {
            var student = await unitOfWork.GetRepository<Student, Guid>()
                 .GetByIdAsync(id);

            if (student is null)
                throw new StudentNotFoundException(id);

            return mapper.Map<StudentReadDto>(student);
        }


      

        public async Task<bool> EnrollInCourseAsync(EnrollmentCreateDto dto)
        {
            var studentRepo = unitOfWork.GetRepository<Student, Guid>();
            var courseRepo = unitOfWork.GetRepository<Course, Guid>();
            var enrollmentRepo = unitOfWork.GetRepositoryWithNoid<StudentEnrollment>();

            var student = await studentRepo.GetByIdAsync(dto.StudentId)
                ?? throw new StudentNotFoundException(dto.StudentId);

            var course = await courseRepo.GetByIdAsync(dto.CourseId)
                ?? throw new CourseNotFoundException(dto.CourseId);

            var enrollment = mapper.Map<StudentEnrollment>(dto);

            await enrollmentRepo.AddAsync(enrollment);

            return await unitOfWork.SaveChangesAsync() > 0;
        }











        public async Task<bool> DeleteAsync(Guid id)
        {
            var repository = unitOfWork.GetRepository<Student, Guid>();
            var student = await repository.GetByIdAsync(id);

            if (student is null)
                throw new StudentNotFoundException(id);

            await repository.DeleteAsync(student);

            return await unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateAsync(StudentCreateDto dto)
        {
            var student = mapper.Map<Student>(dto);
            var repository = unitOfWork.GetRepository<Student, Guid>();

            await repository.AddAsync(student);
           

            return await unitOfWork.SaveChangesAsync()>0;
        }

        public async Task<bool> UpdateAsync(Guid id, StudentCreateDto dto)
        {

            var repository = unitOfWork.GetRepository<Student, Guid>();
            var student = await repository.GetByIdAsync(id);

            if (student is null)
                throw new StudentNotFoundException(id);

           









            mapper.Map(dto, student);
            await repository.UpdateAsync(student);


            return await unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<StudentReadDto> GetByEmailAsync(string email)
        {
            var spec = new StudentWithDetailsSpecification(email:email);

            var student = await unitOfWork.GetRepository<Student, Guid>()
                .GetByIdAsync(spec);

            if (student is null)
                throw new NotFoundException(email);

            return mapper.Map<StudentReadDto>(student);
        }
    }
}
