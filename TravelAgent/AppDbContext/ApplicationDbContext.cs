using Microsoft.EntityFrameworkCore;
using TravelAgent.Model;

namespace TravelAgent.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferLocation> OfferLocations { get; set; }
        public DbSet<OfferType> OfferTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagOffer> TagOffers { get; set; }
        public DbSet<TransportationType> TransportationTypes { get; set; }
    }
}
