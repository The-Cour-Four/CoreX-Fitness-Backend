namespace Project.DTO
{
    public class UserRegisterDTO
    {
        /*Defining the data that the user will send (DTO)*/
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
