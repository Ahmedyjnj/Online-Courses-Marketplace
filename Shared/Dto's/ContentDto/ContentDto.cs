using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.ContentDto
{
    public class ContentDto
    {
        [Required(ErrorMessage = "Video content is required.")]
        public VideoDto VideoDto { get; set; } = new VideoDto();

        [Required(ErrorMessage = "Image content is required.")]
        public ImageDto? ImageDto { get; set; }

        public Guid? CourseId { get; set; }
    }
}
