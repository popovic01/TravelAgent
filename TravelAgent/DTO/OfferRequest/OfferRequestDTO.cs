using TravelAgent.Model;

namespace TravelAgent.DTO.OfferRequest
{
    //zahtev ponude od klijenta
    public class OfferRequestDTO
    {
        public double MaxPrice { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TransportationTypeId { get; set; }
        public List<int> LocationIds { get; set; }
        public int SpotNumber { get; set; } //za koliko ljudi je ponuda
        public int ClientId { get; set; } 
    }
}
