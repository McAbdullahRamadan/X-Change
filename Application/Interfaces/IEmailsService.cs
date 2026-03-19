namespace Application.Interfaces
{
    public interface IEmailsService
    {
        public Task<string> SendEmails(string email, string Message, string? reason);
    }
}
