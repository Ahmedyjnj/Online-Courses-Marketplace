using Domain.Models.Courses;
using Shared.Dto_s.CourseDto;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class StudentViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Profile Photo")]
        public string? PhotoUrl { get; set; }

        [Display(Name = "Rating")]
        public double? Rating { get; set; }

        [Display(Name = "Registered On")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        public IEnumerable<CourseReadDto>? Courses { get; set; }
    }
}
