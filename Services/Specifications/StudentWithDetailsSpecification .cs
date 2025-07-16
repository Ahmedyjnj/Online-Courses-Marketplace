using Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class StudentWithDetailsSpecification : BaseSpecification<Student, Guid>
    {
        public StudentWithDetailsSpecification(Guid? id = null, bool includeEnrollments = false, bool includePayments = false)
            : base(id.HasValue ? s => s.Id == id : null)
        {
            if (includeEnrollments)
            {
                AddInclude(s => s.Enrollments);
                AddInclude(s => s.Enrollments.Select(e => e.Course));
            }

            if (includePayments)
            {
                AddInclude(s => s.Payments);
                AddInclude(s => s.Payments.Select(p => p.Payment));
            }
        }
    }

}
