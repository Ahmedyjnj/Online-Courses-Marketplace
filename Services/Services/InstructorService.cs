using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Courses;
using Domain.Models.Instructors;
using Services.Specifications;
using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class InstructorService(IUnitOfWork unitOfWork, IMapper mapper) : IInstructorService
    {

        public async Task<InstructorReadDto> GetByIdAsync(Guid id)
        {
            var instructor = await unitOfWork.GetRepository<Instructor, Guid>()
                .GetByIdAsync(id);
            if (instructor is null)
                throw new InstructorNotFoundException(id);

            return mapper.Map<InstructorReadDto>(instructor);
        }


        public async Task<IEnumerable<InstructorReadDto>> GetAllAsync()
        {
            var instructors = await unitOfWork.GetRepository<Instructor, Guid>()
                .GetAllAsync();
            return mapper.Map<IEnumerable<InstructorReadDto>>(instructors);
        }

        public async Task<IEnumerable<InstructorSimpleDto>> GetSimpleListAsync()
        {
            var instructors = await unitOfWork.GetRepository<Instructor, Guid>()
                .GetAllAsync();

            return mapper.Map<IEnumerable<InstructorSimpleDto>>(instructors);
        }




        public async Task<bool> CreateAsync(InstructorCreateDto dto)
        {
            var instructor = mapper.Map<Instructor>(dto);

            var repository = unitOfWork.GetRepository<Instructor, Guid>();
            repository.AddAsync(instructor);

            return await unitOfWork.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var repository = unitOfWork.GetRepository<Instructor, Guid>();
            var instructor = await repository.GetByIdAsync(id);

            if (instructor is null)
                throw new InstructorNotFoundException(id);


            repository.DeleteAsync(instructor);

            return await unitOfWork.SaveChangesAsync() > 0;
        }



        public async Task<bool> UpdateAsync(Guid id, InstructorCreateDto dto)
        {
            var repository = unitOfWork.GetRepository<Instructor, Guid>();
            var existingInstructor = await repository.GetByIdAsync(id);

            if (existingInstructor is null)
                throw new InstructorNotFoundException(id);

            mapper.Map(dto, existingInstructor);


            repository.UpdateAsync(existingInstructor);

            return await unitOfWork.SaveChangesAsync() > 0;
        }

        public  async Task<InstructorReadDto> GetByEmailAsync(string email )
        {
            var spec =new InstructorWithDetailsSpecification(email:email);

            var instructor = await unitOfWork.GetRepository<Instructor, Guid>()
               .GetByIdAsync(spec);

            if (instructor is null)
                throw new NotFoundException(email);

            return mapper.Map<InstructorReadDto>(instructor);
        }
    }
}
