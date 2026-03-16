using Domain.Entities.System;

namespace Domain.Entities.Business
{
    public class UserPhoto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
        public PhotoType PhotoType { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime UploadedAt { get; set; }
        public string? FileName { get; set; }
        public long FileSize { get; set; }


        public ApplicationUser User { get; set; }
    }
    public enum PhotoType
    {
        Profile = 1,
        Cover = 2
    }
}
