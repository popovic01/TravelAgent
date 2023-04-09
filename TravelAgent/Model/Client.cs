namespace TravelAgent.Model
{
    public class Client : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNo { get; set; }
        public string PhoneNo { get; set; }
        public List<Offer> Offers { get; set; }
    }
}
