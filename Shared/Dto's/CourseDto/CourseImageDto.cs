using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.CourseDto
{
    public class CourseImageDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Url { get; set; }
        public string? AltText { get; set; }
    }
}
