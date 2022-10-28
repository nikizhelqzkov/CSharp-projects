namespace MyMicroservice.DTOModels
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public int CustomerId { get; set; }
    }
}
