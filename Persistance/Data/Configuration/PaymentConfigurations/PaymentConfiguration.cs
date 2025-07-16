using Domain.Models.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Payments.Payment;

namespace Persistance.Data.Configuration.PaymentConfigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder
                .Property(p => p.Type)
                .HasConversion(new EnumToStringConverter<PaymentType>());





            builder
                .HasIndex(p => p.PaymentDate);

        }
    }
}
