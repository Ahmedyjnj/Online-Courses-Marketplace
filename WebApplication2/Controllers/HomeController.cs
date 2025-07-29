using System.Diagnostics;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.Extensions.Logging;
using Abstraction;
using Services.Services;
using WebApplication2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Domain.Models.Identity;
using static Domain.Models.Identity.ApplicationUser;


namespace WebApplication2.Controllers;


public class HomeController(ILogger<HomeController> _logger, IServiceManager serviceManager, UserManager<ApplicationUser> userManager) : Controller
{
   
    public async Task<IActionResult> Index(string? search)
    {
        var courses = await serviceManager.CourseService.GetAllAsync(search);
        if (courses is null || !courses.Any())
        {
            ViewData["CourseState"] = "No courses found.";
            
        }


        var instructors = await serviceManager.InstructorService.GetSimpleListAsync();


        var viewModel = new HomeViewModel
        {
            Courses = courses,
            Instructors = instructors
        };
       
       
        return View(viewModel);
    }

    //[Authorize]
    //[HttpGet]
    //public async Task<IActionResult> Personal()
    //{
    //    //// Optional: pass data to the view like user's name or role
    //    //ViewData["Message"] = "Welcome to your personal dashboard!";
    //    var currentUser = await userManager.GetUserAsync(User);
    //    if (currentUser != null)
    //    {
    //        var email = currentUser.Email;

    //        if (currentUser.userType == UserType.Student)
    //        {
    //            var student = await serviceManager.StudentService.GetByEmailAsync(email);

    //            return View(student);
    //        }
    //        else if (currentUser.userType == UserType.Instructor)
    //        {
    //            var instructor = await serviceManager.InstructorService.GetByEmailAsync(email);

    //            return View(instructor);
    //        }
    //    }


    //    return View();
    //}




}
