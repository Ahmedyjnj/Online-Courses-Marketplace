using Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Payments
{
    public class StudentPayment
    {
        public Guid StudentEnrollmentId  { get; set; }

        public virtual StudentEnrollment StudentEnrollment { get; set; }

       
        public int ProgressPayment { get; set; }

        public string Notes { get; set; }




        public Guid PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
    }
}
