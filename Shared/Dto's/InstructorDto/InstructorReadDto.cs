using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.InstructorDto
{
    public class InstructorReadDto : InstructorCreateDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
