using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public class PaymentNotFoundException(Guid id):NotFoundException($"Payment with id {id} not found")
    {

    }
}
