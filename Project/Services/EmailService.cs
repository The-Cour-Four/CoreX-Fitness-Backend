using MailKit.Net.Smtp;
using MimeKit;

namespace Project.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationEmailAsync(string toEmail, string userName, string verificationToken)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                emailSettings["SenderName"],
                emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress(userName, toEmail));
            message.Subject = "Verify your email address. ";

            var verificationLink = $"https://corex-fitness-backend-btcjekajg6a2a7ex.francecentral-01.azurewebsites.net/api/Authentication/verifyEmail?email={Uri.EscapeDataString(toEmail)}&token={verificationToken}";

            message.Body = new TextPart("html")
            {
                Text = $@"
                <h2>Welcome to CoreX Fitness, {userName}!</h2>
                <p>Please verify your email by clicking the link below:</p>
                <a href='{verificationLink}'>Verify Email</a>
                
                <p>This link expires in 1 hour.</p>"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                emailSettings["SmtpServer"],
                int.Parse(emailSettings["SmtpPort"]),
                MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                emailSettings["SenderEmail"],
                emailSettings["SenderPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendMotivationalEmailAsync(string toEmail, string userName, string motivationalContent)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
        emailSettings["SenderName"],
        emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress(userName, toEmail));
            message.Subject = "Your Weekly Motivation from CoreX Fitness! 💪 ";

            message.Body = new TextPart("html")
            {
                Text = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
                .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
                .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>CoreX Fitness</h1>
                    <p>Your Fitness Journey Continues!</p>
                </div>
                <div class='content'>
                    <h2>Hey {userName}! 👋</h2>
                    {motivationalContent}
                    <p><strong>Keep crushing your goals!</strong></p>
                </div>
                <div class='footer'>
                    <p>© {DateTime.Now.Year} CoreX Fitness. All rights reserved.</p>
                    <p>Stay fit, stay healthy!</p>
                </div>
            </div>
        </body>
        </html>"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                emailSettings["SmtpServer"],
                int.Parse(emailSettings["SmtpPort"]),
                MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                emailSettings["SenderEmail"],
                emailSettings["SenderPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
    
}
