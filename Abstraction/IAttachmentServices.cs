using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
   public interface IAttachmentServices
    {
       

        public Task<string> UploadImage(String TypeName,IFormFile file);
        //public Task<string> UploadVideo(string? courseid, IFormFile file, string InstructorId);
        public Task<bool> DeletePhoto(string filePath);
        public Task<bool> DeleteVideoAsync(string url);
        public  Task<string> UploadVideoAsync(IFormFile file);
    }
}
