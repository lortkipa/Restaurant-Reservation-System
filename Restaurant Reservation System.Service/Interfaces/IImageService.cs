using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile? image, string folderName, string webRootPath);
        Task DeleteImage(string? imagePath, string webRootPath);
    }
}
