﻿using Microsoft.AspNetCore.Http;

namespace EventBooking.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
        Task DeleteFileAsync(string fileName, string folderName);
    }
}
