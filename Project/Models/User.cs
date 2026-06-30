using Microsoft.AspNetCore.Identity;



namespace Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string passwordHashed { get; set; }
        public DateTime creationDate { get; set; } = DateTime.Now;

        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }

        public bool IsEmailVerified { get; set; } = false;

        public string? EmailVerificationToken { get; set; }

        public DateTime? EmailVerificationTokenExpiry { get; set; }


        public int FailedLoginAttempts { get; set; } = 0;

        //stores when the lockout expires (null = not locked).
        public DateTime? LockoutEnd {  get; set; }
        public DateTime? LastFailedLoginAttempt { get; set; }

        public short Gender { get; set; } = 1;
    }
}