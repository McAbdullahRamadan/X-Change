using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface ILocalStorageService
    {
        Task<LocalFileResult> SaveFileAsync(IFormFile file, string folder);
        Task<bool> DeleteFileAsync(string filePath);
        string GetFileUrl(string relativePath);
    }
    public class LocalFileResult
    {
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public string Url { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public string PublicId { get; set; }
    }
}
