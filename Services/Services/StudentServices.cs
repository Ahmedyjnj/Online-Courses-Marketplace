using Abstraction;
using Shared.Dto_s.EnrollmentDto;
using Shared.Dto_s.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class StudentServices : IStudentService
    {
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task EnrollInCourseAsync(EnrollmentCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentReadDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentReadDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> RegisterAsync(StudentCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, StudentCreateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
