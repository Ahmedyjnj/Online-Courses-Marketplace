using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Courses
{
    public class CourseImage : ModelBase<Guid>
    {
        public string Url { get; set; } = null!;
        public string AltText { get; set; }

        // علاقة مع Course
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
