using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Courses;
using Domain.Models.Identity;
using Domain.Models.Instructors;

using Domain.Models.Payments;
using Domain.Models.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Domain.Models.Payments.Payment;

namespace Persistance.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
      


     
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

      

        public DbSet<Payment> Payments { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }


        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }

        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<InstructorPayment> InstructorPayments { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
            //this for any who inherit from public interface IEntityTypeConfiguration
           


        }


    }
}
