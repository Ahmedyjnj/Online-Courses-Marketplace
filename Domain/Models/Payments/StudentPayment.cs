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
        public Guid StudentId { get; set; }
        public Guid PaymentId { get; set; }
        public int ProgressPayment { get; set; }

        public string Notes { get; set; }

        // Fully qualify the 'Student' type to avoid ambiguity  
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }


        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
    }
}
