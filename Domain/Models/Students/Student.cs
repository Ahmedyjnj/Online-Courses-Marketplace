using Domain.Models.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Students
{
    public class Student : ModelBase<Guid>
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? Rating { get; set; }


        public DateTime RegistrationDate { get; set; }


        public string? PhotoUrl { get; set; }


        public virtual ICollection<StudentEnrollment> Enrollments { get; set; }

        public virtual ICollection<StudentPayment> Payments { get; set; }
    }
}
