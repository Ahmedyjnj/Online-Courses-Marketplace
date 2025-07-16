using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.PaymentDto
{
    public class PaymentReadDto : PaymentCreateDto
    {
        public Guid Id { get; set; }
    }
}
