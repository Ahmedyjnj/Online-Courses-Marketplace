using Abstraction;
using Domain.Models.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s.CourseDto;
using Shared.Dto_s.InstructorDto;
using Shared.Dto_s.StudentDto;
using System.Threading.Tasks;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class InstructorController(IServiceManager serviceManager) : Controller
    {

        [HttpGet]
 
        public async Task<IActionResult> Profile(Guid? id)
        {
            if (id is null)
            {
                var forignIdClaim = User.FindFirst("ForignId")?.Value;

                if (string.IsNullOrEmpty(forignIdClaim) || !Guid.TryParse(forignIdClaim, out var parsedId))
                {
                    return Forbid("Invalid or missing ForignId claim.");
                }

                id = parsedId;
            }


            var Instructor = await serviceManager.InstructorService.GetByIdAsync((Guid)id);

            if (Instructor == null)
            {
                return NotFound("no page to you as instructor");
            }

            var model = new InstructorViewModel
            {
                Id = id,
                Name = Instructor.Name,
                Country = Instructor.Country,
                DateOfBirth = Instructor.DateOfBirth,
                Description = Instructor.Description,
                Email = Instructor.Email,
                Gender = Instructor.Gender,

                Phone = Instructor.Phone,
                PhotoUrl = Instructor.PhotoUrl,
                ResumeUrl = Instructor.ResumeUrl,
            };

            var Courses = await serviceManager.CourseService.GetCourseswithInstructorId((Guid)id);

            model.Courses = Courses;

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var forignIdClaim = User.FindFirst("ForignId")?.Value;
            var Currentid = Guid.Parse(forignIdClaim);

            if (Currentid != id)
            {
                return Forbid();
            }

            if (id is null)
            {
                forignIdClaim = User.FindFirst("ForignId")?.Value;

                if (string.IsNullOrEmpty(forignIdClaim) || !Guid.TryParse(forignIdClaim, out var parsedId))
                {
                    return Unauthorized("Invalid or missing user claim.");
                }

                id = parsedId;
            }

            var instructor = await serviceManager.InstructorService.GetByIdAsync(id.Value);

            if (instructor is null)
            {
                return NotFound("Student not found.");
            }


            var updateDto = new InstructorCreateDto
            {

                Country = instructor.Country,
                DateOfBirth = instructor.DateOfBirth,
                Description = instructor.Description,
                Email = instructor.Email,
                Gender = instructor.Gender,
                Id = instructor.Id,
                Name = instructor.Name,
                Phone = instructor.Phone,
                PhotoUrl = string.IsNullOrEmpty(instructor.PhotoUrl) ? null : Path.GetFileName(instructor.PhotoUrl),
                ResumeUrl = instructor.ResumeUrl,


            };

            return View(updateDto);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InstructorCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Photofile is not null && dto.Photofile.Length > 0)
            {








                dto.PhotoUrl =await serviceManager.AttachmentService.UploadImage("Instructors", dto.Photofile);
            }




            var result = await serviceManager.InstructorService.UpdateAsync((Guid)dto.Id, dto);


            if (result is false)
                return StatusCode(500, "An error occurred while updating.");

            return RedirectToAction(nameof(Profile), new { id = dto.Id });
        }
    





        [HttpGet]
        [Authorize(Roles ="Instructor")]
        public async Task<IActionResult> AddCourse()
        {
            var forignIdClaim = User.FindFirst("ForignId")?.Value;
            var Currentid = Guid.Parse(forignIdClaim);



            




            var updateDto = new CourseCreateDto
            {

                InstructorId = (Guid)Currentid,
                StartDate = DateTime.Now,

            };

            return View(updateDto);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse(CourseCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Photofile is not null && dto.Photofile.Length > 0)
            {

                dto.PhotoUrl = await serviceManager.AttachmentService.UploadImage("Courses", dto.Photofile);
            }

            var result=await serviceManager.CourseService.CreateAsync(dto);

            if (result is false)
                return StatusCode(500, "An error occurred while creating.");

            return RedirectToAction(nameof(Profile), new { id = dto.InstructorId });


        }



















    }
}



