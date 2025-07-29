using Abstraction;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Domain.Models.Instructors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Attachments
{
    public class AttachmentServices(Cloudinary _cloudinary) : IAttachmentServices
    {

        private readonly Dictionary<string, string> ImageExtensionToFolderMap = new()
        {
            [".jpg"] = "Images",
            [".jpeg"] = "Images",
            [".png"] = "Images",
            [".gif"] = "Images"
        };

        private const int ImageMaxSize = 50 * 1024 * 1024; // 50 MB

        public async Task<string> UploadImage(string? typeName, IFormFile file)
        {
            // Validate file
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            // Validate extension
            if (!ImageExtensionToFolderMap.ContainsKey(extension))
                throw new ArgumentException("Invalid image file extension.");

            // Validate size
            if (file.Length > ImageMaxSize)
                throw new ArgumentException("Image exceeds 50 MB size limit.");



            // Prepare storage path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),
                                         "wwwroot",
                                         "Content",
                                         "Images",
                                         typeName ?? "");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            // Save file
            using var stream = new FileStream(filePath, FileMode.Create);
             await file.CopyToAsync(stream);

            return fileName;
        }
        private const int VideoMaxSize = 1024 * 1024 * 1024;  // 1 GB
        private readonly Dictionary<string, string> VideoExtensionToFolderMap = new()
        {
            [".mp4"] = "Videos",
            [".avi"] = "Videos",
            [".mov"] = "Videos",
            [".mkv"] = "Videos"
        };


        //public async Task<string> UploadVideo(string? courseid, IFormFile file, string InstructorId)
        //{
        //    if (string.IsNullOrWhiteSpace(InstructorId))
        //        throw new ArgumentException("InstructorId is required.");

        //    if (string.IsNullOrWhiteSpace(courseid))
        //        throw new ArgumentException("CourseId is required.");
        //    // Validate file existence
        //    if (file == null || file.Length == 0)
        //        throw new ArgumentException("No file uploaded.");

        //    // Validate extension
        //    var extension = Path.GetExtension(file.FileName).ToLower();

        //    if (!VideoExtensionToFolderMap.ContainsKey(extension))
        //        throw new ArgumentException("Invalid video file extension. Supported: .mp4, .avi, .mov, .webm, .mkv");

        //    // Validate size
        //    if (file.Length > VideoMaxSize)
        //        throw new ArgumentException($"Video exceeds {VideoMaxSize / (1024 * 1024)} MB size limit.");

        //    // Prepare storage path
        //    var folderPath = Path.Combine(Directory.GetCurrentDirectory(),
        //                                 "attachments",
        //                                 "videos"
                                         
        //                                 );

        //    Directory.CreateDirectory(folderPath);

            
        //    var fileName = Path.GetFileName(file.FileName)+extension;


        //    var filePath = Path.Combine(folderPath, fileName);

        //    // Save file
        //    using var stream = new FileStream(
        //         filePath,
        //         FileMode.Create,
        //         FileAccess.Write,
        //         FileShare.None,
        //         bufferSize: 4096,          // 4KB optimal buffer
        //         FileOptions.Asynchronous    // Enable async I/O
        //     );

        //    await file.CopyToAsync(stream);

        //    return fileName;
        //}




        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "course_videos" // optional: organize uploads
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                return uploadResult.SecureUrl.ToString();
            else
                throw new Exception("Video upload failed");
        }








        public async Task<bool> DeletePhoto(string filePath)
        {
            // Ensure path is within wwwroot/Files to prevent accidental deletion
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return  true;
            }
            return false;

        }

        public async Task<bool> DeleteVideoAsync(string url)
        {
            try
            {
                // Extract the public ID from the full URL
                var publicId = ExtractPublicIdFromUrl(url);

                var deletionParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Video // VERY IMPORTANT for videos
                };

                var result = await _cloudinary.DestroyAsync(deletionParams);

                return true;
            }
            catch
            {
                return false;
            }
        }
        private string ExtractPublicIdFromUrl(string url)
        {
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split('/');

            // Get path after "upload/"
            int uploadIndex = Array.IndexOf(segments, "upload");
            if (uploadIndex == -1 || uploadIndex + 1 >= segments.Length)
                throw new ArgumentException("Invalid Cloudinary URL");

            // Combine all segments after "upload/" and remove extension
            var publicPath = string.Join("/", segments.Skip(uploadIndex + 1));
            var publicId = Path.Combine(Path.GetDirectoryName(publicPath) ?? "", Path.GetFileNameWithoutExtension(publicPath));

            return publicId.Replace("\\", "/"); // Always use forward slashes
        }
    }

}
