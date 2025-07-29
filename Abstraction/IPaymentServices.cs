using Shared.Dto_s.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IPaymentServices
    {
        Task<string> StartPayment(decimal amount, string email, string phone, string name, string userid, string Courseid);

        string CalculateHmac(string secretKey, string message);

        Task<Guid> CreateAsync(PaymentCreateDto dto);

        Task<bool> LinkPaymentToInstructor(InstructorPaymentDto instructorPaymentDto);

        Task<bool> LinkPaymentToStudent(LinkPaymentToStudent linkPaymentToStudent);

    }
}
