using Domain.Models.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configuration.StudentConfigurations
{
    public class StudentEnrollmentConfiguration : IEntityTypeConfiguration<StudentEnrollment>
    {
        public void Configure(EntityTypeBuilder<StudentEnrollment> builder)
        {

            builder 
               .HasKey(e=>e.Id);

            builder.HasOne(se => se.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(se => se.StudentId);

            builder.HasOne(se => se.Course)
            .WithMany(c => c.Enrollments)
             .HasForeignKey(se => se.CourseId);

            builder.HasMany(e => e.studentPayments)
                .WithOne(e => e.StudentEnrollment)
                .HasForeignKey(e=>e.StudentEnrollmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(se => new { se.StudentId, se.CourseId }).IsUnique();
        }
    }
}
