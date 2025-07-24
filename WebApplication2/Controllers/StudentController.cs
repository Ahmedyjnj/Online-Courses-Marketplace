using Abstraction;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s.StudentDto;
using System.ComponentModel.DataAnnotations;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class StudentController(IServiceManager serviceManager) : Controller
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
            

            var student = await serviceManager.StudentService.GetByIdAsync((Guid)id);

            if (student is null)
            {
                return NotFound("No page to you as student");
            }

            var model = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone=student.Phone,
                Gender=student.Gender,
                DateOfBirth = student.DateOfBirth,
                PhotoUrl = student.PhotoUrl,
                Rating = student.Rating,
                RegistrationDate = student.RegistrationDate
            };

            
            var courses = await serviceManager.CourseService.GetByStudentIdAsync(student.Id.Value);


            model.Courses= courses;


            return View(model);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var forignIdClaim = User.FindFirst("ForignId")?.Value;
            var Currentid=Guid.Parse(forignIdClaim);

            if (Currentid !=id )
            {
                return Forbid(); 
            }

            if (id is null)
            {
                forignIdClaim = User.FindFirst("ForignId")?.Value;

                if (string.IsNullOrEmpty(forignIdClaim) || !Guid.TryParse(forignIdClaim, out var parsedId))
                {
                    return Forbid("Invalid or missing ForignId claim.");
                }

                id = parsedId;
            }

            var student = await serviceManager.StudentService.GetByIdAsync(id.Value);

            if (student is null)
            {
                return NotFound("Student not found.");
            }

            
            var updateDto = new StudentCreateDto
            {
                Id = student.Id ?? Guid.Empty,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Gender = student.Gender,
                DateOfBirth = student.DateOfBirth,
                PhotoUrl= string.IsNullOrEmpty(student.PhotoUrl) ? null : Path.GetFileName(student.PhotoUrl)
            };

            return View(updateDto); // أو return PartialView إذا ستستخدمه داخل Modal
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Photofile is not null && dto.Photofile.Length > 0)
            {
               
                

                

                

               
                dto.PhotoUrl = serviceManager.AttachmentService.UploadFile("Students",dto.Photofile);
            }




            var result = await serviceManager.StudentService.UpdateAsync((Guid)dto.Id,dto);

         
            if (result is false)
                return StatusCode(500, "An error occurred while updating.");

            return RedirectToAction(nameof(Profile), new { id = dto.Id });
        }
    }
}
