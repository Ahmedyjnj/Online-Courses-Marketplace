using Abstraction;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using static Domain.Models.Identity.ApplicationUser;
using System.Security.Claims;

namespace WebApplication2.Claims
{
    public class CustomClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor, IServiceProvider serviceProvider,IServiceManager serviceManager) : UserClaimsPrincipalFactory<ApplicationUser>(userManager,optionsAccessor)
    {
       

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("UserType", user.userType.ToString()));

           
            

            if (user.ForignId != null)
            {
                identity.AddClaim(new Claim("ForignId", user.ForignId.ToString()));




                if (user.userType == UserType.Student)
                {
                    var student = await serviceManager.StudentService.GetByIdAsync(user.ForignId.Value);
                    identity.AddClaim(new Claim("DisplayName", student?.Name ?? ""));
                    identity.AddClaim(new Claim("PhotoUrl", student?.PhotoUrl ?? ""));
                    identity.AddClaim(new Claim("PersonId", student?.Id.ToString() ?? ""));
                }
                else if (user.userType == UserType.Instructor)
                {
                    var instructor = await serviceManager.InstructorService.GetByIdAsync(user.ForignId.Value);
                    identity.AddClaim(new Claim("DisplayName", instructor?.Name ?? ""));
                    identity.AddClaim(new Claim("PhotoUrl", instructor?.PhotoUrl ?? ""));
                    identity.AddClaim(new Claim("PersonId", instructor?.Id.ToString() ?? ""));
                }

            }
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }



            return identity;
        }
    }
}
