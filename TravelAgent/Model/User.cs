namespace TravelAgent.Model
{
    public class User
    {
        public int Id { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Username { get; set; }
    }
}
