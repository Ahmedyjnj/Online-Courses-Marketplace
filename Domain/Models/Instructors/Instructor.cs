using Domain.Models.Courses;
using Domain.Models.Identity;
using Domain.Models.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Instructors
{
    public class Instructor : ModelBase<Guid>
    {
        
        public string Name { get; set; }
        public string Email { get; set; }

        public string? Country { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; } = false;

        public string? ResumeUrl { get; set; }
        public string? PhotoUrl { get; set; } 
        public string? Description { get; set; }


        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<InstructorPayment> InstructorPayments { get; set; }

        
      

    }
}
