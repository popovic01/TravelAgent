﻿namespace TravelAgent.DTO.Offer
{
    public class OfferReviewDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public double Rating { get; set; }
        public int OfferTypeId { get; set; }
        public int TransportationTypeId { get; set; }
    }
}