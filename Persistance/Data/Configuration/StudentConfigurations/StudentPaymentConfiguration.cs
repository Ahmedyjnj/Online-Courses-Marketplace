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
                .HasKey(sp => new { sp.StudentEnrollmentId, sp.PaymentId });

            builder.HasOne(e => e.StudentEnrollment)
                .WithMany(e=>e.studentPayments)
                .HasForeignKey(sp=>sp.StudentEnrollmentId);

           
            builder.HasOne(sp => sp.Payment)
                   .WithMany(p => p.StudentPayments)
                   .HasForeignKey(sp => sp.PaymentId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.Property(sp => sp.ProgressPayment)
              .IsRequired();


        }
    }
}
