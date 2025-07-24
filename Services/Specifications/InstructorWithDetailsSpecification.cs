using Domain.Models.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Specifications
{
    public class InstructorWithDetailsSpecification : BaseSpecification<Instructor, Guid>
    {
        public InstructorWithDetailsSpecification(Guid? id = null, string? name = null,string?email=null, bool includeCourses = false, bool includepayments = false) //false due to performance
            : base(c => 
            (!id.HasValue || c.Id == id.Value) &&
            (string.IsNullOrWhiteSpace(name) || c.Name.ToLower().Contains(name)) &&
            (string.IsNullOrWhiteSpace(email) || c.Email==email))
        {
            if (includeCourses)
            {
                AddInclude(i => i.Courses);
               

               
            }
            if (includepayments)
            {
               

                
                AddInclude(i => i.InstructorPayments.Select(ip => ip.Payment));
            }
        }
    }
}
