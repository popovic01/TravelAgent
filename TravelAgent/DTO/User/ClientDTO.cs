namespace TravelAgent.DTO.User
{
    public class ClientDTO : UserRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNo { get; set; }
        public string PhoneNo { get; set; }
    }
}
