using Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Specifications
{
    public class StudentWithDetailsSpecification : BaseSpecification<Student, Guid>
    {
        public StudentWithDetailsSpecification(Guid? id = null, string? name = null, string? email = null, bool includeEnrollments = false, bool includePayments = false)
            : base(c =>
            (!id.HasValue || c.Id == id.Value) &&
            (string.IsNullOrWhiteSpace(name) || c.Name.ToLower().Contains(name))&&
            (string.IsNullOrWhiteSpace(email) || c.Email==email))

        {
            if (includeEnrollments)
            {
              
                AddInclude(s => s.Enrollments.Select(e => e.Course));
            }

            if (includePayments)
            {
                
                AddInclude(s => s.StudentPayments.Select(p => p.Payment));
            }
        }
    }

}
