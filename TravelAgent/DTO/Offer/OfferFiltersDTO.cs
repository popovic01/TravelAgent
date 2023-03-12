namespace TravelAgent.DTO.Offer
{
    public class OfferFiltersDTO
    {
        public string SearchFilter { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<int> LocationIds { get; set; }
        public List<int> TagIds { get; set; }
        public int ClientId { get; set; } //id of logged in user, because we want to show all offers that particular user has in his wishlist
    }
}
