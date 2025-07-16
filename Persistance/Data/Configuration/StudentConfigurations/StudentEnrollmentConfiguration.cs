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
               .HasKey(e => new { e.StudentId, e.CourseId });
        }
    }
}
