namespace TravelAgent.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Offer> Offers { get; set; }
    }
}
