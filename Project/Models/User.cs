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
        public string Gender { get; set; } = string.Empty;

    }
}
