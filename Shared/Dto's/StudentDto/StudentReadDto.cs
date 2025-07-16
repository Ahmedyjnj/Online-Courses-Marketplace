using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.StudentDto
{
    public class StudentReadDto : StudentCreateDto
    {
        public Guid Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public double? Rating { get; set; }
    }
}
