using Application.Responses;
using Domain.Entities.Business;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.PhotoUsers.Command.Models
{
    public class UploadPhotoCommand : IRequest<DataResponse<PhotoDto>>
    {
        // 👇 ضيف constructor فاضي
        public UploadPhotoCommand() { }

        // 👇 أو constructor كامل
        public UploadPhotoCommand(PhotoType photoType, IFormFile file, string userId)
        {
            PhotoType = photoType;
            File = file;
            UserId = userId;
        }

        public PhotoType PhotoType { get; set; }
        public IFormFile File { get; set; }
        public string UserId { get; set; }
    }







    public class PhotoDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public PhotoType PhotoType { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime UploadedAt { get; set; }
        public string FileName { get; set; }
        public string FormattedDate { get; set; }
        public long FileSize { get; set; }
        public string PublicId { get; set; }
    }
}
