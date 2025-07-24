using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Instructors;
using Domain.Models.Payments;
using Domain.Models.Students;
using Services.Specifications;
using Shared.Dto_s.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PaymentService(IUnitOfWork unitOfWork,IMapper mapper) : IPaymentService
    {
        public async Task<bool> CreatePaymentAsync(PaymentCreateDto dto)
        {
            var payment = mapper.Map<Payment>(dto);

            

            var repo = unitOfWork.GetRepository<Payment, Guid>();

            await repo.AddAsync(payment);

            var result = await unitOfWork.SaveChangesAsync();

            return result > 0 ;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var repo = unitOfWork.GetRepository<Payment, Guid>();
            var payment = await repo.GetByIdAsync(id);

            if (payment is null)
                throw new PaymentNotFoundException(id);

            await repo.DeleteAsync(payment);
           return await unitOfWork.SaveChangesAsync()>0;
        }

        public async Task<IEnumerable<PaymentReadDto>> GetAllPayments()
        {
            var payments = await unitOfWork.GetRepository<Payment, Guid>().GetAllAsync();
            return mapper.Map<IEnumerable<PaymentReadDto>>(payments);
        }

        public async Task<PaymentReadDto> GetPaymentByIdAsync(Guid id)
        {
            var payment = await unitOfWork.GetRepository<Payment, Guid>().GetByIdAsync(id);

            if (payment is null)
                throw new PaymentNotFoundException(id);

            return mapper.Map<PaymentReadDto>(payment);
        }

        public async Task<IEnumerable<PaymentReadDto>> GetStudentPayments(Guid studentId)
        {
            var spec = new StudentWithDetailsSpecification(id:studentId,includePayments:true);

            var student = await unitOfWork.GetRepository<Student, Guid>()
                .GetByIdAsync(spec);

            if (student is null)
                throw new StudentNotFoundException(studentId);

            var payments = student.StudentPayments.Select(p => p.Payment);

            return mapper.Map<IEnumerable<PaymentReadDto>>(payments);
        }

        public async Task<bool> LinkPaymentToInstructor(InstructorPaymentDto instructorPaymentDto)
        {
            var payment = await unitOfWork.GetRepository<Payment, Guid>().GetByIdAsync(instructorPaymentDto.PaymentId)
                 ?? throw new PaymentNotFoundException(instructorPaymentDto.PaymentId);

            var instructor = await unitOfWork.GetRepository<Instructor, Guid>().GetByIdAsync(instructorPaymentDto.InstructorId)
                ?? throw new InstructorNotFoundException(instructorPaymentDto.InstructorId);

            var instructorPayment = mapper.Map<InstructorPayment>(instructorPaymentDto);

            await unitOfWork.GetRepositoryWithNoid<InstructorPayment>().AddAsync(instructorPayment);
           return await unitOfWork.SaveChangesAsync()>0;
        }

        public async Task<bool> LinkPaymentToStudent(LinkPaymentToStudent linkPaymentToStudent)
        {
            var paymentId = linkPaymentToStudent.PaymentId;
            var studentId = linkPaymentToStudent.Studentid;
            var courseId = linkPaymentToStudent.CourseId;
            

            var payment = await unitOfWork.GetRepository<Payment, Guid>().GetByIdAsync(paymentId)
        ?? throw new PaymentNotFoundException(paymentId);

            var enrollmentRepo = unitOfWork.GetRepositoryWithNoid<StudentEnrollment>();
            var enrollment = (await enrollmentRepo.GetAllAsync())
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment is null) 
            { 
                enrollment = new StudentEnrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.UtcNow,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await enrollmentRepo.AddAsync(enrollment);
            }

            var studentPayment = new StudentPayment
            {
                StudentEnrollment = enrollment,
                PaymentId = paymentId,
                ProgressPayment = linkPaymentToStudent.ProgressPayment
            };

           

            await unitOfWork.GetRepositoryWithNoid<StudentPayment>().AddAsync(studentPayment);
           return await unitOfWork.SaveChangesAsync()>0;
        }
    }
}
