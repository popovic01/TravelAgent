namespace TravelAgent.Model
{
    public class TagOffer
    {
        public int Id { get; set; }
        public Offer Offer { get; set; }
        public Tag Tag { get; set; }
    }
}
