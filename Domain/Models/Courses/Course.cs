using Domain.Models.Instructors;

using Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Courses
{
    public class Course : ModelBase<Guid>
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? PhotoUrl { get; set; }
        public Guid InstructorId { get; set; }       
        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<StudentEnrollment> Enrollments { get; set; }
        

        public virtual ICollection<CourseImage> Images { get; set; }
        public virtual ICollection<CourseVideo> Videos { get; set; }


    }
}
