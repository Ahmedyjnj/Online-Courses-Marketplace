using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IInstructorService
    {
        Task<InstructorReadDto> GetByIdAsync(Guid id);
        Task<InstructorReadDto> GetByEmailAsync(string email);
        Task<IEnumerable<InstructorReadDto>> GetAllAsync();
        Task<bool> CreateAsync(InstructorCreateDto dto);
        Task<bool> UpdateAsync(Guid id, InstructorCreateDto dto);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<InstructorSimpleDto>> GetSimpleListAsync();
        
    }
}
