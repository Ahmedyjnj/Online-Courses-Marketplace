using Shared.Dto_s.EnrollmentDto;
using Shared.Dto_s.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IStudentService
    {
        Task<StudentReadDto> GetByIdAsync(Guid id);

        Task<StudentReadDto> GetByEmailAsync(string email);

        Task<IEnumerable<StudentReadDto>> GetAllAsync();
        Task<bool> CreateAsync(StudentCreateDto dto);
        Task<bool> UpdateAsync(Guid id, StudentCreateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> EnrollInCourseAsync(EnrollmentCreateDto dto);


    }
}
