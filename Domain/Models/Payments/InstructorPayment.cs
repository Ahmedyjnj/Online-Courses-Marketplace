using Domain.Models.Instructors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Payments
{
    public class InstructorPayment
    {
        public Guid InstructorId { get; set; }
        public Guid PaymentId { get; set; }

        public int ProgressPayment { get; set; }
        public string Notes { get; set; }

        [ForeignKey("InstructorId")]
        public virtual Instructor Instructor { get; set; }

        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
    }
}
