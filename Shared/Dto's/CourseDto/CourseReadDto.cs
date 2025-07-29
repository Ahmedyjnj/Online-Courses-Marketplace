using Shared.Dto_s.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.CourseDto
{
    public class CourseReadDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime StartDate { get; set; }

        public string? PhotoUrl { get; set; }
        public DateTime EndDate { get; set; }

        public int Price { get; set; }  

        public Guid InstructorId { get; set; }
        public InstructorSimpleDto? Instructor { get; set; }

    }
}
