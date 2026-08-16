using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Models.Zoos;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data
{
    public class WebContext : DbContext
    {
        public DbSet<AnimalFamilyData> AnimalFamilies { get; set; }

        public DbSet<AnimalSpeciesData> AnimalSpecies { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<CommentData> Comments { get; set; }

        public DbSet<PromotionData> Promotions { get; set; }

        public DbSet<TicketData> Tickets { get; set; }

        public DbSet<ZooData> Zoos { get; set; }

        public WebContext(DbContextOptions<WebContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalFamilyData>()
                .HasMany(x => x.AnimalSpecies)
                .WithOne(x => x.AnimalFamily)
                .HasForeignKey(x => x.AnimalFamilyId);

            modelBuilder.Entity<AnimalFamilyData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedAnimalFamilies)
                .HasForeignKey(x => x.CreatorId);

            modelBuilder.Entity<AnimalSpeciesData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedAnimalSpecies)
                .HasForeignKey(x => x.CreatorId);

            modelBuilder.Entity<CommentData>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.AuthorId);
            
            modelBuilder.Entity<CommentData>()
                .HasOne(x => x.Zoo)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ZooId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PromotionData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedPromotions)
                .HasForeignKey(x => x.CreatorId);
            
            modelBuilder.Entity<PromotionData>()
                .HasOne(x => x.Venue)
                .WithMany(x => x.Promotions)
                .HasForeignKey(x => x.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketData>()
                .HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.UserId);
            
            modelBuilder.Entity<TicketData>()
                .HasOne(x => x.Zoo)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.ZooId);

            modelBuilder.Entity<ZooData>()
                .HasMany(x => x.AnimalSpecies)
                .WithMany(x => x.Zoos)
                .UsingEntity(x => x.ToTable("zoo_species_bindings"));

            modelBuilder.Entity<ZooData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedZoos)
                .HasForeignKey(x => x.CreatorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
