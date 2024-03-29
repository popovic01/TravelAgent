﻿namespace TravelAgent.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ReservationCode { get; set; }
        public DateTime Date { get; set; }
        public Offer Offer { get; set; }
        public Client Client { get; set; }
        //for stripe
        public string? SessionId { get; set; }
        public string? PaymentIntent { get; set; }
        public bool Paid { get; set; }
    }
}
