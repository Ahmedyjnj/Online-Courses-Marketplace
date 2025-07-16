using Domain.Contracts;
using Domain.Models.Courses;
using Domain.Models.Instructors;
using Domain.Models.Payments;
using Domain.Models.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Data
{
    public class Dbinializer(AppDbContext context) : IDbinializer
    {
        public async  Task InitializeAsync()
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            try
            {
                // Seed Courses
               

                // Seed Instructors
                if (!context.Instructors.Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistance\Data\Seeds\instructors.json");
                    var objects = JsonSerializer.Deserialize<List<Instructor>>(data);
                    if (objects is not null && objects.Any())
                    {
                        context.Instructors.AddRange(objects);
                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Courses.Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistance\Data\Seeds\courses.json");
                    var objects = JsonSerializer.Deserialize<List<Course>>(data);
                    if (objects is not null && objects.Any())
                    {
                        context.Courses.AddRange(objects);
                        await context.SaveChangesAsync();
                    }
                }

                // Seed Students
                if (!context.Students.Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistance\Data\Seeds\students.json");
                    var objects = JsonSerializer.Deserialize<List<Student>>(data);
                    if (objects is not null && objects.Any())
                    {
                        context.Students.AddRange(objects);
                        await context.SaveChangesAsync();
                    }
                }

                // Seed Payments 
                if (!context.Payments.Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Persistance\Data\Seeds\payments.json");
                    var objects = JsonSerializer.Deserialize<List<Payment>>(data);
                    if (objects is not null && objects.Any())
                    {
                        context.Payments.AddRange(objects);
                        await context.SaveChangesAsync();
                    }
                }






                if (!context.StudentEnrollments.Any())
                {
                    var student1Id = Guid.Parse("e1d18f4c-0ef3-4dc3-93cf-7b758e7f81a5");
                    var student2Id = Guid.Parse("a5c905ee-9c4c-4b58-b72d-c2b7f1c16f0f");

                    var course1Id = Guid.Parse("d4e5f6a7-3333-3333-3333-333333333333");
                    var course2Id = Guid.Parse("05d3102c-8446-47f6-8a62-060c3da77b29");

                    var student1 = await context.Students.FindAsync(student1Id);
                    var student2 = await context.Students.FindAsync(student2Id);
                    var course1 = await context.Courses.FindAsync(course1Id);
                    var course2 = await context.Courses.FindAsync(course2Id);

                    if (student1 != null && course1 != null)
                    {
                        context.StudentEnrollments.Add(new StudentEnrollment
                        {
                            StudentId = student1Id,
                            CourseId = course1Id,
                            EnrollmentDate = DateTime.UtcNow,
                            Status = "Active",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        });
                    }

                    if (student2 != null && course2 != null)
                    {
                        context.StudentEnrollments.Add(new StudentEnrollment
                        {
                            StudentId = student2Id,
                            CourseId = course2Id,
                            EnrollmentDate = DateTime.UtcNow,
                            Status = "Active",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        });
                    }

                    await context.SaveChangesAsync();
                }





                if (!context.StudentPayments.Any())
                {
                    var student1Id = Guid.Parse("e1d18f4c-0ef3-4dc3-93cf-7b758e7f81a5");
                    var student2Id = Guid.Parse("a5c905ee-9c4c-4b58-b72d-c2b7f1c16f0f");

                    var payment1Id = Guid.Parse("6ca65466-ad33-43a6-ba30-11eff0866822");
                    var payment2Id = Guid.Parse("b669c8b6-c907-4148-a0e4-225a08901d57");

                    var student1 = await context.Students.FindAsync(student1Id);
                    var student2 = await context.Students.FindAsync(student2Id);
                    var payment1 = await context.Payments.FindAsync(payment1Id);
                    var payment2 = await context.Payments.FindAsync(payment2Id);

                    if (student1 != null && payment1 != null)
                    {
                        context.StudentPayments.Add(new StudentPayment
                        {
                            StudentId = student1Id,
                            PaymentId = payment1Id,
                            ProgressPayment = 50,
                            Notes = "Initial payment for course enrollment"
                        });
                    }

                    if (student2 != null && payment2 != null)
                    {
                        context.StudentPayments.Add(new StudentPayment
                        {
                            StudentId = student2Id,
                            PaymentId = payment2Id,
                            ProgressPayment = 30,
                            Notes= "Payment for course materials and access"
                        });
                    }

                    await context.SaveChangesAsync();
                }


                if (!context.InstructorPayments.Any())
                {
                    var instructor1Id = Guid.Parse("a1b2c3d4-1111-1111-1111-111111111111");
                    var instructor2Id = Guid.Parse("b2c3d4e5-2222-2222-2222-222222222222");

                    var payment1Id = Guid.Parse("590cc82e-7eb6-4ab4-8f65-ad768ebdf9cc");
                    var payment2Id = Guid.Parse("a4ed2866-4cc2-4653-a49e-451c97d4be26");

                    var instructor1 = await context.Instructors.FindAsync(instructor1Id);
                    var instructor2 = await context.Instructors.FindAsync(instructor2Id);
                    var payment1 = await context.Payments.FindAsync(payment1Id);
                    var payment2 = await context.Payments.FindAsync(payment2Id);

                    if (instructor1 != null && payment1 != null)
                    {
                        context.InstructorPayments.Add(new InstructorPayment
                        {
                            InstructorId = instructor1Id,
                            PaymentId = payment1Id,
                            ProgressPayment = 100,
                            Notes = "Initial payout for month profits delivery"
                        });
                    }

                    if (instructor2 != null && payment2 != null)
                    {
                        context.InstructorPayments.Add(new InstructorPayment
                        {
                            InstructorId = instructor2Id,
                            PaymentId = payment2Id,
                            ProgressPayment = 50,
                            Notes = "Payout as begin of teaching"
                        });
                    }

                    await context.SaveChangesAsync();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding database: {ex.Message}");
                throw;
            }
        }

        
    }
}
