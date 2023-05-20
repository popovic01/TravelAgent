namespace TravelAgent.DTO.Reservation
{
    public class ReservationResponseDTO
    {
        public string ReservationCode { get; set; }
        public DateTime Date { get; set; }
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int ClientId { get; set; }
    }
}
