using Shared.Dto_s.CourseDto;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class InstructorViewModel
    {

        public Guid? Id { get; set; }

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

        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Resume")]
        public string? ResumeUrl { get; set; }

        [Display(Name = "Profile Photo")]
        public string? PhotoUrl { get; set; }

        [Display(Name = "Short Bio / Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }


        public IEnumerable<CourseReadDto>? Courses { get; set; }
    }
}
