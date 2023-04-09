namespace TravelAgent.DTO.Reservation
{
    public class ReservationDTO
    {
        public string ReservationCode { get; set; }
        public DateTime Date { get; set; }
        public int OfferId { get; set; }
        public int ClientId { get; set; }
    }
}
