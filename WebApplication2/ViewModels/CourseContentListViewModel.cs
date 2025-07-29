using Shared.Dto_s.ContentDto;

namespace WebApplication2.ViewModels
{
    public class CourseContentListViewModel
    {
       
        public Guid CourseId { get; set; }

        public int? Price { get; set; }

        public IEnumerable<ContentDto>? Contents { get; set; }
    }
}
