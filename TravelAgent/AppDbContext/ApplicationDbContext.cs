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
        public DbSet<OfferType> OfferTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagOffer> TagOffers { get; set; }
        public DbSet<TransportationType> TransportationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>()
                .HasMany(o => o.Locations)
                .WithMany(l => l.Offers)
                .UsingEntity<Dictionary<string, object>>(
                    "OfferLocation",
                    j => j.HasOne<Location>().WithMany().HasForeignKey("LocationId"),
                    j => j.HasOne<Offer>().WithMany().HasForeignKey("OfferId"),
                    j =>
                    {
                        j.Property<int>("OfferLocationId").ValueGeneratedOnAdd();
                        j.HasKey("OfferLocationId");
                    });
        }
    }
}
