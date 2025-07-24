using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Payments
{
    public class Payment : ModelBase<Guid>
    {
       
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public enum PaymentType { withdraw, deposit }
        public PaymentType Type { get; set; }

       

        public virtual ICollection<StudentPayment> StudentPayments { get; set; }
        public virtual ICollection<InstructorPayment> InstructorPayments { get; set; }

    }
}
