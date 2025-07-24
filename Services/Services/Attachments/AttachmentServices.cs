using Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Attachments
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly Dictionary<string, string> ExtensionToFolderMap = new()
        {
            // Images
            [".jpg"] = "Images",
            [".jpeg"] = "Images",
            [".png"] = "Images",
            [".gif"] = "Images",
            // Videos
            [".mp4"] = "Videos",
            [".avi"] = "Videos",
            [".mov"] = "Videos",
            [".webm"] = "Videos"
        };

        private const int ImageMaxSize = 50 * 1024 * 1024;        // 50 MB
        private const int VideoMaxSize =  1024 * 1024 * 1024;  // 1 GB

        public string UploadFile(string? TypeName,IFormFile file)
        {
            
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!ExtensionToFolderMap.ContainsKey(extension))
                throw new Exception("Invalid file extension.");

            var folderName = ExtensionToFolderMap[extension];

            var isVideo = folderName == "Videos";


            if (isVideo && file.Length > VideoMaxSize)
                throw new Exception("Video exceeds 2 GB size limit.");

            if (!isVideo && file.Length > ImageMaxSize)
                throw new Exception("Image exceeds 50 MB size limit.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Content", folderName,TypeName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }

        public bool DeleteFile(string filePath)
        {
            // Ensure path is within wwwroot/Files to prevent accidental deletion
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;

        }
    }

}
