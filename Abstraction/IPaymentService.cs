using Shared.Dto_s.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IPaymentService
    {
        Task<bool> CreatePaymentAsync(PaymentCreateDto dto);
        Task<PaymentReadDto> GetPaymentByIdAsync(Guid id);
        Task<IEnumerable<PaymentReadDto>> GetAllPayments();
        Task<bool> LinkPaymentToStudent(LinkPaymentToStudent LinkPaymentToStudent);

        Task<bool> LinkPaymentToInstructor(InstructorPaymentDto instructorPaymentDto);

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<PaymentReadDto>> GetStudentPayments(Guid studentId);

    }
}
