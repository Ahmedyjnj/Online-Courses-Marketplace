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
        Task<Guid> CreatePaymentAsync(PaymentCreateDto dto);
        Task<PaymentReadDto> GetPaymentByIdAsync(Guid id);
        Task<IEnumerable<PaymentReadDto>> GetAllPayments();
        Task LinkPaymentToEnrollment(Guid paymentId, Guid enrollmentId);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<PaymentReadDto>> GetStudentPayments(Guid studentId);

    }
}
