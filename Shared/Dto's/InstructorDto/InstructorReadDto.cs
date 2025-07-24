using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.InstructorDto
{
    public class InstructorReadDto : InstructorCreateDto
    {
        
        public bool IsActive { get; set; }
    }
}
