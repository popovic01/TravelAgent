namespace TravelAgent.Model
{
    public class OfferLocation
    {
        public int Id { get; set; }
        public Offer Offer { get; set; }
        public Location Location { get; set; }
    }
}
