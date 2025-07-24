using Shared.Dto_s.CourseDto;
using Shared.Dto_s.InstructorDto;

namespace WebApplication2.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<CourseReadDto>? Courses { get; set; }
        public IEnumerable<InstructorSimpleDto>? Instructors { get; set; }
    }
}
