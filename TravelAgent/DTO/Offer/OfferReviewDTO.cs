namespace TravelAgent.DTO.Offer
{
    public class OfferReviewDTO : OfferDTO
    {
        public string DepartureLocation { get; set; }
        public string Description { get; set; }
        public string OfferType { get; set; }
        public int? Id { get; set; }
    }
}
