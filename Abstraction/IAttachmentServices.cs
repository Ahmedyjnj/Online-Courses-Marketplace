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
       

        public string UploadFile(String TypeName,IFormFile file);

        public bool DeleteFile(string filePath);

    }
}
