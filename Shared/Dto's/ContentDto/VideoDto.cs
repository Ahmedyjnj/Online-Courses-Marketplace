using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.ContentDto
{
   public class VideoDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Video URL is required.")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public IFormFile? Videofile { get; set; }
    }
}
