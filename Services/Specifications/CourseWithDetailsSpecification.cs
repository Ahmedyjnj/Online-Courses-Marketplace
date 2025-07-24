using Domain.Models.Courses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Specifications
{
    public class CourseWithDetailsSpecification : BaseSpecification<Course, Guid>
    {
        public CourseWithDetailsSpecification(Guid? id = null,Guid? InstructorId=null, string? name = null, bool includeEnrollments = false, bool includeInstructors = false , bool includeContent = false)
        : base(c=>
            (!id.HasValue || c.Id == id.Value) &&
            (string.IsNullOrWhiteSpace(name) || c.Title.ToLower().Contains(name)) &&
            (!InstructorId.HasValue || c.InstructorId == InstructorId.Value))
        {
           

            if (includeEnrollments)
            {

                AddInclude(c => c.Enrollments);
               
               
            }

            if (includeInstructors)
            {
                AddInclude(c => c.Instructor);
                
            }
            if (includeContent)
            {
                AddInclude(c => c.Images);
                AddInclude(c => c.Videos);
            }
        }
               
    }
}
