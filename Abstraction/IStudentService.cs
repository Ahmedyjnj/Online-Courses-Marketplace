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
        Task<IEnumerable<StudentReadDto>> GetAllAsync();
        Task<Guid> RegisterAsync(StudentCreateDto dto);
        Task UpdateAsync(Guid id, StudentCreateDto dto);
        Task DeleteAsync(Guid id);
        Task EnrollInCourseAsync(EnrollmentCreateDto dto);


    }
}
