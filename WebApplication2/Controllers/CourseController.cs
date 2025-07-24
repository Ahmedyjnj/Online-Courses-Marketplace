using Abstraction;
using AutoMapper;
using Domain.Models.Instructors;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s.CourseDto;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    public class CourseController(IServiceManager serviceManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var course = await serviceManager.CourseService.GetAllAsync();

            return View(course);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
         {
            

            var course=await serviceManager.CourseService.GetByIdAsync(id);

            var forignIdClaim = User.FindFirst("ForignId")?.Value;

            if (!Guid.TryParse(forignIdClaim, out var userId))
            {
                return Unauthorized("Invalid or missing user claim.");
            }


            var updatedcourse = new CourseCreateDto
            {
                Id = id,
                Category = course.Category,
                Description = course.Description,
                EndDate = course.EndDate,
                InstructorId=course.InstructorId,
                Title = course.Title,
                PhotoUrl = string.IsNullOrEmpty(course.PhotoUrl) ? null : Path.GetFileName(course.PhotoUrl),
                StartDate = course.StartDate,
                
            };

           
            return View(updatedcourse);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model is not null && model.Photofile?.Length > 0)
            {
                model.PhotoUrl = serviceManager.AttachmentService.UploadFile("Courses", model.Photofile);
            }

            

            var result = await serviceManager.CourseService.UpdateAsync((Guid)model.Id,model);

            if (result is false)
            {
                return StatusCode(500, "An error occurred while creating.");
            }


            return RedirectToAction(nameof(Profile),nameof(Instructor));
        }






        public async Task<IActionResult> Details(Guid id)
        {

            var course = await serviceManager.CourseService.GetAllAsync();

            return View(course);
        }
    }
}
