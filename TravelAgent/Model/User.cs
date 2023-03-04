namespace TravelAgent.Model
{
    public class User
    {
        public int Id { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] LozinkaHash { get; set; }
        public string Email { get; set; }
    }
}
