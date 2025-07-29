using Abstraction;
using Domain.Models.Identity;
using Domain.Models.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Dto_s.InstructorDto;
using Shared.Dto_s.StudentDto;
using WebApplication2.ViewModels;
using static Domain.Models.Identity.ApplicationUser;
using System.Security.Cryptography;
using System.Text;
using Shared.Dto_s.PaymentDto;



namespace WebApplication2.Controllers
{
    public class PaymentController(IServiceManager serviceManager, IConfiguration configuration) : Controller
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> StartPayment(Guid id)
        {

            var course = await serviceManager.CourseService.GetByIdAsync(id);
            if (course == null)
                return NotFound("Course not found");

            var forignId = Guid.Parse(User.FindFirst("ForignId")?.Value);

            if (forignId == null)
            {
                return NotFound("log in error user not found");
            }

            var viewmodel = new PaymentViewModel
            {
                Name = course.Title,

                Price = course.Price,

            };
            var userType = User.FindFirst("UserType")?.Value;

            if (userType == ApplicationUser.UserType.Student.ToString())
            {


                var student = await serviceManager.StudentService.GetByIdAsync(forignId);
                viewmodel.Email = student.Email;
                viewmodel.Phone = student.Phone;

            }
            else if (userType == ApplicationUser.UserType.Instructor.ToString())
            {
                var instructor = await serviceManager.InstructorService.GetByIdAsync(forignId);
                viewmodel.Email = instructor.Email;
                viewmodel.Phone = instructor.Phone;

            }


            viewmodel.CourseId = id.ToString();
            viewmodel.UserId = forignId.ToString();


            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> StartPayment(PaymentViewModel model)
        {
            var iframeUrl = await serviceManager.PaymentService.StartPayment(
                model.Price,
                model.Email,
                model.Phone,
                model.Name,
                model.UserId,
                model.CourseId
                );

            return Redirect(iframeUrl);
        }


        [HttpPost]
        public async Task<ActionResult> PaymentCallback()
        {
            // قراءة الـ JSON القادم من Paymob
            string rawJson;
            using (var reader = new StreamReader(Request.Body))
            {
                rawJson = reader.ReadToEnd();
            }
            // 2. verify HMAC
            var sentHmac = Request.Headers["hmac"].ToString().ToLower();
            var data = JsonConvert.DeserializeObject<PaymobCallbackData>(rawJson);
            if (data == null) return BadRequest();

            var fields = new[]
               {
                    data.amount_cents,
                    data.created_at,
                    data.currency,
                    data.error_occured,
                    data.has_parent_transaction,
                    data.id,
                    data.integration_id,
                    data.is_3d_secure,
                    data.is_auth,
                    data.is_capture,
                    data.is_refunded,
                    data.is_standalone_payment,
                    data.is_voided,
                    data.order.id,
                    data.owner,
                    data.pending,
                    data.source_data.pan,
                    data.source_data.sub_type,
                    data.source_data.type,
                    data.success
                };

            var concatenated = string.Concat(fields);

            // مفتاح الـ HMAC من appsettings
            var secretKey = configuration["PaymobSettings:IntegrationId"];

            var computedHmac = serviceManager.PaymentService.CalculateHmac(secretKey, concatenated);

            if (computedHmac != sentHmac.ToLower())
                return new StatusCodeResult(403);

            var merchantOrderId = data.merchant_order_id;        
            var parts = merchantOrderId?.Split('_');
            if (parts?.Length != 2)
                return BadRequest("Bad merchant_order_id");

            var userId = parts[0];
            var courseId = parts[1];

            var payment = new PaymentCreateDto
            {
                id = Guid.NewGuid(),
                Amount = decimal.Parse(data.amount_cents) / 100m,
                PaymentDate = DateTime.UtcNow,                   // or parse data.created_at
                PaymentMethod = data.source_data.type,             // e.g. "CARD"
                PaymentReference = data.order.id,                     // transaction id
               
                                   
            };

            var paymentId=await serviceManager.PaymentService.CreateAsync(payment);

            var userType = User.FindFirst("UserType")?.Value;

            if (userType == ApplicationUser.UserType.Student.ToString())
            {


                var student = await serviceManager.StudentService.GetByIdAsync(Guid.Parse(userId));
                var link = new LinkPaymentToStudent
                {
                    Studentid=Guid.Parse(userId),
                    PaymentId= (Guid)paymentId
                };

               await serviceManager.PaymentService.LinkPaymentToStudent(link);
            }
            else if (userType == ApplicationUser.UserType.Instructor.ToString())
            {
                var instructor = await serviceManager.InstructorService.GetByIdAsync(Guid.Parse(userId));

                var link = new InstructorPaymentDto
                {
                   InstructorId= Guid.Parse(userId),
                    PaymentId = (Guid)paymentId
                };

                await serviceManager.PaymentService.LinkPaymentToInstructor(link);
            }
            return RedirectToAction("Index", "Home");
        }




      
                
    }
}
