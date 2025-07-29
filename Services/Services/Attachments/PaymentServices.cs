using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Courses;
using Domain.Models.Instructors;
using Domain.Models.Payments;
using Domain.Models.Students;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Dto_s.CourseDto;
using Shared.Dto_s.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Attachments
{
    public class PaymentServices(IConfiguration configuration, HttpClient httpClient, IUnitOfWork unitOfWork, IMapper mapper) : IPaymentServices
    {
        public async Task<string> StartPayment(decimal amount, string email, string phone, string name, string userid, string Courseid)
        {
            var _apiKey = configuration["PaymobSettings:ApiKey"];
            var _integrationId = int.Parse(configuration["PaymobSettings:IntegrationId"]);
            var _iframeId = int.Parse(configuration["PaymobSettings:IframeId"]);


            var authResponse = await httpClient.PostAsync(
                "https://accept.paymob.com/api/auth/tokens",

                new StringContent(JsonConvert.SerializeObject(new { api_key = _apiKey }),
                 Encoding.UTF8, "application/json"));


            if (!authResponse.IsSuccessStatusCode)
                throw new Exception("Failed to authenticate with Paymob.");


            var authResult = JsonConvert.DeserializeObject<dynamic>(await authResponse.Content.ReadAsStringAsync());

            string token = authResult.token;

            var orderPayload = new
            {
                auth_token = token,
                delivery_needed = false,
                amount_cents = (int)(amount * 100),


                merchant_order_id = $"{userid}_{Courseid}_{DateTime.UtcNow.Ticks}",

                items = new object[] {
                 new
                    {
                        name = "Course Purchase",
                        amount_cents = (int)(amount * 100),
                        description = $"Course ID: {Courseid}, User ID: {userid}",
                        quantity = 1
                    }}
            };

            var orderResponse = await httpClient.PostAsync(
             "https://accept.paymob.com/api/ecommerce/orders",
             new StringContent(JsonConvert.SerializeObject(orderPayload), Encoding.UTF8, "application/json"));


            
            
            if (!orderResponse.IsSuccessStatusCode)
                throw new Exception("Failed to create Paymob order. " + orderResponse);


            var orderResult = JsonConvert.DeserializeObject<dynamic>(await orderResponse.Content.ReadAsStringAsync());
            int orderId = orderResult.id;

            var paymentKeyPayload = new
            {
                auth_token = token,
                amount_cents = (int)(amount * 100),
                expiration = 3600,
                order_id = orderId,
                billing_data = new
                {
                    first_name = name,
                    last_name = "User",
                    email = email,
                    phone_number = phone,
                    city = "Cairo",
                    country = "EG",
                    apartment = "NA",
                    floor = "NA",
                    street = "NA",
                    building = "NA",
                    state = "NA"
                },
                currency = "EGP",
                integration_id = _integrationId
            };


            var paymentKeyResponse = await httpClient.PostAsync(
            "https://accept.paymob.com/api/acceptance/payment_keys",
            new StringContent(JsonConvert.SerializeObject(paymentKeyPayload), Encoding.UTF8, "application/json"));


            if (!paymentKeyResponse.IsSuccessStatusCode)
                throw new Exception("Failed to get Paymob payment key.");

            var paymentKeyResult = JsonConvert.DeserializeObject<dynamic>(await paymentKeyResponse.Content.ReadAsStringAsync());
            string paymentToken = paymentKeyResult.token;


            string iframeUrl = $"https://accept.paymob.com/api/acceptance/iframes/{_iframeId}?payment_token={paymentToken}";
            return iframeUrl;
        }

        public string CalculateHmac(string secretKey, string message)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hash = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public async Task<bool> LinkPaymentToInstructor(InstructorPaymentDto instructorPaymentDto)
        {
            var payment = await unitOfWork.GetRepository<Payment, Guid>().GetByIdAsync(instructorPaymentDto.PaymentId)
                 ?? throw new PaymentNotFoundException(instructorPaymentDto.PaymentId);

            var instructor = await unitOfWork.GetRepository<Instructor, Guid>().GetByIdAsync(instructorPaymentDto.InstructorId)
                ?? throw new InstructorNotFoundException(instructorPaymentDto.InstructorId);

            var instructorPayment = mapper.Map<InstructorPayment>(instructorPaymentDto);

            await unitOfWork.GetRepositoryWithNoid<InstructorPayment>().AddAsync(instructorPayment);
            return await unitOfWork.SaveChangesAsync() > 0;
        }




        public async Task<string> GetPaymentStatus(string merchantOrderId)
        {
            var tokenResponse = await httpClient.PostAsync(
                "https://accept.paymob.com/api/auth/tokens",
                new StringContent(JsonConvert.SerializeObject(new { api_key = configuration["PaymobSettings:ApiKey"] }), Encoding.UTF8, "application/json"));

            if (!tokenResponse.IsSuccessStatusCode)
                throw new Exception("Unable to authenticate with Paymob");

            var token = JsonConvert.DeserializeObject<dynamic>(await tokenResponse.Content.ReadAsStringAsync()).token;

            var url = $"https://accept.paymob.com/api/ecommerce/orders?merchant_order_id={merchantOrderId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Unable to retrieve order info");

            var orderDetails = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            var transactions = orderDetails.data[0].transactions;
            if (transactions.Count == 0)
                return "No Transaction";

            string status = transactions[0].success == true ? "Paid" : "Failed";
            return status;
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
            return await unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<Guid> CreateAsync(PaymentCreateDto dto)
        {
            var payment = mapper.Map<Payment>(dto);

            await unitOfWork.GetRepository<Payment, Guid>().AddAsync(payment);

             await unitOfWork.SaveChangesAsync() ;

            return payment.Id;

        }
    }
}


