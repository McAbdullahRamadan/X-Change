namespace Application.Services.Impelementation
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string EnvironmentVariable { get; set; } = "CLOUDINARY_URL";

        // التحقق من صحة الإعدادات
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(CloudName) &&
                   !string.IsNullOrEmpty(ApiKey) &&
                   !string.IsNullOrEmpty(ApiSecret);
        }
    }
}
