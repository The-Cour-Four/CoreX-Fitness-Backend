namespace Project.DTO
{
    public class EmailVerificationDTO
    {
        public string Email { get; set; } = string.Empty;

        public string Token { get; set; }
    }
}
