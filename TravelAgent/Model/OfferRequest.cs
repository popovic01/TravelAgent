namespace TravelAgent.Model
{
    public class OfferRequest
    {
        public int Id { get; set; }
        public double MaxPrice { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TransportationType TransportationType { get; set; }
        public List<Location> Locations { get; set; }
        public int SpotNumber { get; set; } //za koliko ljudi je ponuda
    }
}
