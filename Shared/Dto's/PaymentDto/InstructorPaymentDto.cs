using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.PaymentDto
{
   public class InstructorPaymentDto 
    {
        public Guid InstructorId { get; set; }
        public Guid PaymentId { get; set; }

        public int ProgressPayment { get; set; }
        public string Notes { get; set; }
    }
}
