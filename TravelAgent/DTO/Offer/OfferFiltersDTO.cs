namespace TravelAgent.DTO.Offer
{
    public class OfferFiltersDTO
    {
        public string SearchFilter { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<int> LocationIds { get; set; }
        public List<int> TagIds { get; set; }
        public int? UserId { get; set; }
        public bool IsWishlist { get; set; }
    }
}
