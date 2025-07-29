using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class PaymentViewModel
    {

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        
        [Display(Name = "total Price")]
        public int Price { get; set; }


        public string UserId { get; set; }

        public string CourseId { get; set; }

    }
}
