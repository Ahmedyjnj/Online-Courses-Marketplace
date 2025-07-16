using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.EnrollmentDto
{
    public class EnrollmentCreateDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
