namespace Project.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
