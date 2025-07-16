using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.PaymentDto
{
    public class PaymentCreateDto
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
    }
}
