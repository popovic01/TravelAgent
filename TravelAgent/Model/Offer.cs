using TravelAgent.DTO.Offer;

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
        public int Duration { get; set; }
        public double Rating { get; set; }
        public string OfferCode { get; set; }
        public OfferType OfferType { get; set; }
        public TransportationType TransportationType { get; set; }
        public int WishlistCount { get; set; }
        public int ReservationCount { get; set; }
        public List<Location> Locations { get; set; }
    }
}
