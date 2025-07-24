using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.InstructorDto
{
    public class InstructorCreateDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Country { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ResumeUrl { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Description { get; set; }

        public IFormFile? Photofile { get; set; }
    }
}
