using Domain.Models.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class PaymentWithDetailsSpecification : BaseSpecification<Payment, Guid>
    {
        public PaymentWithDetailsSpecification(Guid? id = null, bool includeStudents = false , bool includeInstructors = false)
            : base(id.HasValue ? p => p.Id == id : null)
        {
            if (includeStudents)
            {
                
                AddInclude(p => p.StudentPayments.Select(sp => sp.StudentEnrollment.StudentId));

            }
            if (includeInstructors)
            {
                

               
                AddInclude(p => p.InstructorPayments.Select(sp => sp.Instructor));
            }
        }
    }
}
