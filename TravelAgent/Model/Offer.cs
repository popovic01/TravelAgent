namespace TravelAgent.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Rating { get; set; }
        public string OfferCode { get; set; }
        public OfferType OfferType { get; set; }
        public TransportationType TransportationType { get; set; }
        public int WishlistCount { get; set; }
        public int ReservationCount { get; set; }
        public int AvailableSpots { get; set; }
        public List<Location> Locations { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Client> Clients { get; set; }
        public int? OfferRequestId { get; set; }
        public OfferRequest? OfferRequest { get; set; }
    }
}
