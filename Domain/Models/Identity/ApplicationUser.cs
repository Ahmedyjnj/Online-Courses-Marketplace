using Domain.Models.Instructors;
using Domain.Models.Students;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Identity
{
    public class ApplicationUser :IdentityUser
    {
       

        public Guid? ForignId { get; set; }


        public enum UserType {Instructor,Student}
           

        public UserType userType { get; set; }


       


    }
    
}
