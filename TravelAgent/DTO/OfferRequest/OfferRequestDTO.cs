namespace TravelAgent.DTO.OfferRequest
{
    //zahtev ponude od klijenta
    public class OfferRequestDTO
    {
        public int? Id { get; set; }
        public double MaxPrice { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TransportationType { get; set; }
        public List<string> Locations { get; set; }
        public int SpotNumber { get; set; } //za koliko ljudi je ponuda
        public int ClientId { get; set; }
        public int? OfferId { get; set; }
    }
}
