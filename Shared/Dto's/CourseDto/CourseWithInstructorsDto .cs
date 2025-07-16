using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.CourseDto
{
    public class CourseWithInstructorsDto : CourseReadDto
    {
        public List<InstructorSimpleDto> Instructors { get; set; }
    }
}
