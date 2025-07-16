using Domain.Models.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configuration.CourseConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {

            builder
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
            .HasIndex(c => c.Title);

            builder
            .HasMany(c => c.Images)
            .WithOne(i => i.Course)
            .HasForeignKey(i => i.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.Videos)
                .WithOne(v => v.Course)
                .HasForeignKey(v => v.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
