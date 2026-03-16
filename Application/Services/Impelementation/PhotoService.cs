using Application.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impelementation
{
    public class PhotoService : IPhotoService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<PhotoService> _logger;
        private readonly ApplicationDbContext _context;

        public PhotoService(
            ILocalStorageService localStorage,
            ILogger<PhotoService> logger, ApplicationDbContext context)
        {
            _localStorage = localStorage;
            _logger = logger;
            _context = context;
        }



        public async Task<PhotoUploadResult> AddPhotoAsync(IFormFile file, string folder = "profiles")
        {
            try
            {
                // حفظ الصورة في wwwroot
                var result = await _localStorage.SaveFileAsync(file, folder);

                return new PhotoUploadResult
                {
                    PublicId = result.PublicId,
                    Url = result.Url,
                    SecureUrl = result.Url, // نفس الرابط
                    Format = Path.GetExtension(result.FileName).TrimStart('.'),
                    Bytes = result.FileSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding photo");
                throw;
            }
        }

        public async Task<bool> DeletePhotoAsync(string publicId)
        {
            try
            {
                // 1. حذف من قاعدة البيانات الأول
                var photo = await _context.UserPhotos.FirstOrDefaultAsync(p => p.PublicId == publicId);

                if (photo != null)
                {
                    _context.UserPhotos.Remove(photo);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Photo deleted from database: {PublicId}", publicId);
                }

                // 2. حذف من wwwroot
                var fileDeleted = await _localStorage.DeleteFileAsync(Path.Combine("uploads", "profiles", publicId));

                if (fileDeleted)
                    _logger.LogInformation("Photo deleted from wwwroot: {PublicId}", publicId);

                return true; // لو حذف من أي مكان
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting photo: {PublicId}", publicId);
                return false;
            }
        }


    }
}
