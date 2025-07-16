using Domain.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configuration.StudentConfigurations
{
    public class StudentPaymentConfiguration : IEntityTypeConfiguration<StudentPayment>
    {
        public void Configure(EntityTypeBuilder<StudentPayment> builder)
        {

            builder
                .HasKey(ep => new { ep.StudentId, ep.PaymentId });
        }
    }
}
