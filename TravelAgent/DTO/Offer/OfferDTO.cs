namespace TravelAgent.DTO.Offer
{
    public class OfferDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public double Rating { get; set; }
        public string OfferType { get; set; }
        public string TransportationType { get; set; }
        public List<int> LocationIds { get; set; }
        public List<int> TagIds { get; set; }
        public int WishlistCount { get; set; } //number of users who have this offer in their wishlist
        public int AvailableSpots { get; set; }
        public int AvailableSpotsLeft { get; set; } //availableSpots - reservationCount

    }
}
