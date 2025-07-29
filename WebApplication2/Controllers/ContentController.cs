using Abstraction;
using Domain.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Services.Services.Attachments;
using Shared.Dto_s.ContentDto;
using System.Threading.Tasks;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class ContentController(IServiceManager serviceManager) : Controller
    {

        //that is what we need to take parameter id of content and
        //then make sure personid enroll in this course
        //we need to call services
        [Authorize]
        [HttpGet]

        public async Task<IActionResult> Index(Guid id)//courseid
        {
            var userType = User.FindFirst("UserType")?.Value;
            var userId = Guid.Parse(User.FindFirst("ForignId")?.Value);

            if (!await serviceManager.ContentServices.AccessState(id, userId, userType))
            {

                return Forbid();
            }




            var course = await serviceManager.CourseService.GetByIdAsync(id);
            if (course == null)
                return NotFound("Course not found");




            var contents = await serviceManager.ContentServices.GetAllAsync(id);

            var viewmodel = new CourseContentListViewModel
            {
                CourseId = id,
                Price = course.Price,
                Contents = contents
            };


            return View(viewmodel);
        }


        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> AddVideo(Guid courseId)
        {


            var userType = User.FindFirst("UserType")?.Value;
            var userId = Guid.Parse(User.FindFirst("ForignId")?.Value);

            if (!await serviceManager.ContentServices.AccessState(courseId, userId, userType))
            {

                return Forbid();
            }

            var id = Guid.NewGuid(); // توليد معرف جديد للمحتوى

            var content = new ContentDto
            {

                CourseId = courseId,
                VideoDto = new VideoDto
                {
                    Id = id,
                    Url = "default.mp4"

                },
                ImageDto = new ImageDto
                {
                    Id = id,
                    Url = "default.jpg",

                }
            };


            return View(content); // أو تعرض خطأ
        }


        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> AddVideo(ContentDto contents)
        {
            var userType = User.FindFirst("UserType")?.Value;
            var userId = Guid.Parse(User.FindFirst("ForignId")?.Value);

            if (!await serviceManager.ContentServices.AccessState((Guid)contents.CourseId, userId, userType))
            {

                return Forbid();
            }

            if (!ModelState.IsValid)
            {

                return View(contents);
            }


            var videoName = await serviceManager.AttachmentService.UploadVideoAsync(contents.VideoDto.Videofile);
            // var videoName =await serviceManager.AttachmentService.UploadVideo(contents.CourseId.ToString(),contents.VideoDto.Videofile,userId);
            var imageName = await serviceManager.AttachmentService.UploadImage("Courses", contents.ImageDto.Photofile); // تحتاج تنفذ UploadImage بنفس منطق UploadVideo

            if (string.IsNullOrWhiteSpace(videoName))
                return BadRequest("Failed to upload video or image");

            contents.VideoDto.Url = videoName;
            contents.ImageDto.Url = imageName;

            var result = await serviceManager.ContentServices.AddContent(contents);

            if (!result)
                return BadRequest("Failed to add content");






            return Ok(new { message = "تم الرفع", redirectUrl = Url.Action("Index", "Content", new { id = contents.CourseId }) });

        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public async Task<IActionResult> UpdateVideo(Guid contentId)
        {
            var content = await serviceManager.ContentServices.GetByIdAsync(contentId);
            if (content == null)
                return NotFound("المحتوى غير موجود.");

            var userId = Guid.Parse(User.FindFirst("ForignId")?.Value ?? Guid.Empty.ToString());
            var userType = User.FindFirst("UserType")?.Value;

            var hasAccess = await serviceManager.ContentServices.AccessState((Guid)content.CourseId, userId, userType);
            if (!hasAccess)
                return Forbid();

            return View(content); // إرسال البيانات لواجهة التعديل
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public async Task<IActionResult> UpdateVideo(ContentDto content)
        {
            var userType = User.FindFirst("UserType")?.Value;
            var userId = Guid.Parse(User.FindFirst("ForignId")?.Value);

            if (!await serviceManager.ContentServices.AccessState((Guid)content.CourseId, userId, userType))
                return Forbid();

            // Check for file existence before processing :}
            try
            {
                if (content.ImageDto?.Photofile != null && content.ImageDto.Photofile.Length > 0)
                {
                    var imageName = await serviceManager.AttachmentService.UploadImage("Courses", content.ImageDto.Photofile);
                    content.ImageDto.Url = imageName;
                }

                if (content.VideoDto?.Videofile != null && content.VideoDto.Videofile.Length > 0)
                {
                    var videoName = await serviceManager.AttachmentService.UploadVideoAsync(content.VideoDto.Videofile);
                    content.VideoDto.Url = videoName;
                }

                bool result = await serviceManager.ContentServices.UpdateContent(content);
                if (!result)
                    return NotFound("Content not updated");

                return Ok(new
                {
                    message = "تم التحديث بنجاح",
                    redirectUrl = Url.Action("Index", "Content", new { id = content.CourseId })
                });
            }
            catch (Exception ex)
            {
                //  log exception
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }



        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public async Task<IActionResult> DeleteVideo(Guid contentId)
        {
            var content = await serviceManager.ContentServices.GetByIdAsync(contentId);
            if (content == null)
                return NotFound("Content not found");

            var userType = User.FindFirst("UserType")?.Value;
            var userId = Guid.Parse(User.FindFirst("ForignId")?.Value);

            if (!await serviceManager.ContentServices.AccessState((Guid)content.CourseId, userId, userType))
            {

                return Forbid();
            }





            return View(content);
        }
        [Authorize(Roles = "Instructor")]
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(ContentDto content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));


            var userType = User.FindFirst("UserType")?.Value;
            var userId = Guid.Parse(User.FindFirst("ForignId")?.Value);

            if (!await serviceManager.ContentServices.AccessState((Guid)content.CourseId, userId, userType))
            {

                return Forbid();
            }


            //delete photo from attachment
            await serviceManager.AttachmentService.DeletePhoto(content.ImageDto.Url);

            //delete video from attachment
            await serviceManager.AttachmentService.DeleteVideoAsync(content.VideoDto.Url);


            var result =  await serviceManager.ContentServices.DeleteContent(content.VideoDto.Id);

            if (!result)
                return BadRequest("Failed to delete content.");

            return RedirectToAction("Index", new { id = content.CourseId });
        }

    }
}
