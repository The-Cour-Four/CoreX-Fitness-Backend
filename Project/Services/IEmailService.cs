namespace Project.Services
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string toEmail, string userName, string verificationToken);
        Task SendMotivationalEmailAsync(string toEmail, string userName, string motivationalContent);
    }
}
