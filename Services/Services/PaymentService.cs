using Abstraction;
using Shared.Dto_s.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<Guid> CreatePaymentAsync(PaymentCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentReadDto>> GetAllPayments()
        {
            throw new NotImplementedException();
        }

        public Task<PaymentReadDto> GetPaymentByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentReadDto>> GetStudentPayments(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public Task LinkPaymentToEnrollment(Guid paymentId, Guid enrollmentId)
        {
            throw new NotImplementedException();
        }
    }
}
