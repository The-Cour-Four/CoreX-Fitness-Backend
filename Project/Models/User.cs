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


    }
}
