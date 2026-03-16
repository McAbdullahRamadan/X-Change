using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impelementation
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<LocalStorageService> _logger;

        public LocalStorageService(
            IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor,
            ILogger<LocalStorageService> logger)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<LocalFileResult> SaveFileAsync(IFormFile file, string folder)
        {
            try
            {
                // 1. التحقق من المدخلات
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is null or empty");

                // 2. إنشاء اسم فريد للملف
                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var publicId = fileName; // نفس الاسم

                // 3. إنشاء المسار
                var webRootPath = _environment.WebRootPath
                    ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var uploadsFolder = Path.Combine(webRootPath, "uploads", folder);

                // 4. إنشاء المجلد إذا مش موجود
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // 5. المسار الكامل للملف
                var filePath = Path.Combine(uploadsFolder, fileName);

                // 6. حفظ الملف
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 7. إنشاء الـ URL
                var relativePath = Path.Combine("uploads", folder, fileName).Replace("\\", "/");
                var url = GetFileUrl(relativePath);

                _logger.LogInformation($"File saved: {filePath}");

                return new LocalFileResult
                {
                    FileName = fileName,
                    RelativePath = relativePath,
                    AbsolutePath = filePath,
                    Url = url,
                    FileSize = file.Length,
                    ContentType = file.ContentType,
                    PublicId = publicId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving file");
                throw;
            }
        }

        public string GetFileUrl(string relativePath)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return $"/{relativePath}";

            return $"{request.Scheme}://{request.Host}/{relativePath}";
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return false;

                var fullPath = Path.Combine(_environment.WebRootPath, filePath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation($"File deleted: {fullPath}");
                    return await Task.FromResult(true);
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file");
                return false;
            }
        }
    }
}
