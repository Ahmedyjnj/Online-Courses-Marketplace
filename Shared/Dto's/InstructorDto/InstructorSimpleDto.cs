using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.InstructorDto
{
    public class InstructorSimpleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string? PhotoUrl { get; set; }
    }
}
