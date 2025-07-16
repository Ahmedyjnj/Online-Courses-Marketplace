using Shared.Dto_s.CourseDto;
using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface ICourseService
    {
        Task<CourseReadDto> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseReadDto>> GetAllAsync();
        Task<bool> CreateAsync(CourseCreateDto dto);
        Task<bool> UpdateAsync(Guid id, CourseCreateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<InstructorSimpleDto>> GetInstructorsByCourseId(Guid courseId);


    }
}
