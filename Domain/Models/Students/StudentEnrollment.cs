
using Domain.Models.Courses;
using Domain.Models.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Students
{
    public class StudentEnrollment
    {
       
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; } // Fully qualify the Course type to resolve ambiguity  
        
        
        
        
        
        
        
        
       
    }
}
