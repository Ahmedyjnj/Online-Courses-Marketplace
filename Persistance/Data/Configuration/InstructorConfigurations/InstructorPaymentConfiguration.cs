using Domain.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configuration.InstructorConfigurations
{
    public class InstructorPaymentConfiguration : IEntityTypeConfiguration<InstructorPayment>
    {
        public void Configure(EntityTypeBuilder<InstructorPayment> builder)
        {

            builder
            .HasKey(ip => new { ip.InstructorId, ip.PaymentId });

            builder
                .HasOne(ip => ip.Instructor)
                .WithMany(i => i.InstructorPayments)
                .HasForeignKey(ip => ip.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(ip => ip.Payment)
                .WithMany(p => p.InstructorPayments)
                .HasForeignKey(ip => ip.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
