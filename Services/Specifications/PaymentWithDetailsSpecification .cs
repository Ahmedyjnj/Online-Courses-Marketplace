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
        public PaymentWithDetailsSpecification(Guid? id = null, bool includeStudents = true)
            : base(id.HasValue ? p => p.Id == id : null)
        {
            if (includeStudents)
            {
                AddInclude(p => p.EnrollmentPayments);
                AddInclude(p => p.EnrollmentPayments.Select(sp => sp.Student));
            }
        }
    }
}
