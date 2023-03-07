namespace TravelAgent.Model
{
    public class Wishlist
    {
        public int Id { get; set; }
        public Offer Offer { get; set; }
        public Client Client { get; set; }
    }
}
