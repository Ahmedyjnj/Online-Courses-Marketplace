using Abstraction;
using Domain.Models.Identity;
using Domain.Models.Students;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Shared.Dto_s.InstructorDto;
using Shared.Dto_s.StudentDto;
using WebApplication2.ViewModels;
using static Domain.Models.Identity.ApplicationUser;
using System.Security.Claims;


namespace WebApplication2.Controllers
{
    public class AccountController(IServiceManager serviceManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : Controller
    {

        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #region Createuser

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName + Guid.NewGuid().ToString(), // Use email as username
                userType = (ApplicationUser.UserType)model.UserType

            };
            //now create row in student or instructor table based on user type

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            #endregion

            try
            {
                #region make instance of person

                if (model.UserType == ApplicationUser.UserType.Student)
                {

                    await userManager.AddToRoleAsync(user, ApplicationUser.UserType.Student.ToString());

                    var student = new StudentCreateDto
                    {
                        Id = Guid.NewGuid(),
                        Name = model.UserName,
                        Email = model.Email,
                        Phone = model.Phone,
                    };
                    await serviceManager.StudentService.CreateAsync(student);
                    user.ForignId = student.Id;
                    await userManager.UpdateAsync(user);
                }
                else if (model.UserType == ApplicationUser.UserType.Instructor)
                {
                    await userManager.AddToRoleAsync(user, ApplicationUser.UserType.Instructor.ToString());

                    var instructor = new InstructorCreateDto
                    {
                        Id = Guid.NewGuid(),

                        Name = model.UserName,

                        Email = model.Email,
                        Phone = model.Phone,

                    };
                    await serviceManager.InstructorService.CreateAsync(instructor);
                    user.ForignId = instructor.Id;
                    await userManager.UpdateAsync(user);
                }

                #endregion
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await userManager.DeleteAsync(user);
                return View(model);
            }






            return RedirectToAction("Login", "Account");
        }




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }



            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();


          



            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View(); // AccessDenied.cshtml
        }

    }
}
