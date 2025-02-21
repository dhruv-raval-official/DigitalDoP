using DigitalDoP.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDoP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define tables
        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascading delete issues
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.User)
                .WithMany() // Assuming no navigation property
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<Service>()
                .HasIndex(s => s.TrackingId)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

    }
}
