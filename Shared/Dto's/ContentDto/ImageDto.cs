using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.ContentDto
{
   public class ImageDto
    {
        [Required(ErrorMessage = "you have problem with the id of image")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string Url { get; set; }

       
        public string? AltText { get; set; }

        public IFormFile? Photofile { get; set; }
    }
}
