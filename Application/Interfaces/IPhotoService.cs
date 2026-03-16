using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
        Task<PhotoUploadResult> AddPhotoAsync(IFormFile file, string folder = "profiles");
        Task<bool> DeletePhotoAsync(string publicId);


    }

    public class PhotoUploadResult
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string SecureUrl { get; set; }
        public string Format { get; set; }
        public long Bytes { get; set; }
    }

}
