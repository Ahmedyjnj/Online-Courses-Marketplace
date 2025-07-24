using AutoMapper;
using Domain.Models.Payments;
using Shared.Dto_s.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile()
        {

            CreateMap<InstructorPayment, InstructorPaymentDto>().ReverseMap();

            CreateMap<Payment,PaymentCreateDto>().ReverseMap();

            CreateMap<Payment, PaymentReadDto>().ReverseMap();

          



        }
    }
}
