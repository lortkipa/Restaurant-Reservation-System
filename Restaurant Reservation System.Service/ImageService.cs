using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImageAsync(IFormFile? image, string folderName, string webRootPath)
        {
            if (image == null || image.Length == 0)
                return null;

            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            string extension = Path.GetExtension(image.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return null;

            string uploadFolder = Path.Combine(webRootPath, "uploads", folderName);
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string uniqueImageName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadFolder, uniqueImageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"/uploads/{folderName}/{uniqueImageName}";
        }
        public async Task DeleteImage(string? imagePath, string webRootPath)
        {

        }
    }
}
