using Azure;
using DigitalAwareness.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalAwareness.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create sequence for referral numbers
            modelBuilder.HasSequence<int>("ReferralNumberSequence")
                .StartsAt(1)
                .IncrementsBy(1);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.ReferralNumber)
                    .HasDefaultValueSql("NEXT VALUE FOR ReferralNumberSequence");

                // Add required configurations
                entity.Property(e => e.Email).IsRequired().HasMaxLength(450);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FatherName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Gender).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Property(e => e.Designation).IsRequired();
                entity.Property(e => e.Village).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Post).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Block).IsRequired().HasMaxLength(100);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.State).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Pincode).IsRequired().HasMaxLength(6);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.ReferralCode).IsRequired();
            });

            // Configure State entity
            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Configure City entity
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(c => c.State)
                      .WithMany(s => s.Cities)
                      .HasForeignKey(c => c.StateId);
            });

            // Seed States and Cities
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            
            // Seed States
           modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "Uttar Pradesh" },
                new State { Id = 2, Name = "Maharashtra" },
                new State { Id = 3, Name = "Delhi" },
                new State { Id = 4, Name = "Karnataka" },
                new State { Id = 5, Name = "West Bengal" }
            );

            // Seed Cities for Uttar Pradesh
            modelBuilder.Entity<City>().HasData(
               

                new City { Id = 1, Name = "Agra", StateId = 1 },
                new City { Id = 2, Name = "Akbarpur", StateId = 1 },
                new City { Id = 3, Name = "Aligarh", StateId = 1 },
               new City { Id = 4, Name = "Amroha", StateId = 1 },
                new City { Id = 5, Name = "Ayodhya(Faizabad)", StateId = 1 },
                new City { Id = 6, Name = "Azamgarh", StateId = 1 },
                new City { Id = 7, Name = "Baheraich", StateId = 1 },
                new City { Id = 8, Name = "Ballia", StateId = 1 },
                new City { Id = 9, Name = "Banda", StateId = 1 },
                new City { Id = 10, Name = "Baraut", StateId = 1 },
                new City { Id = 61, Name = "Bareilly", StateId = 1 },
                new City { Id = 62, Name = "BAsti", StateId = 1 },
                new City { Id = 63, Name = "Budaun", StateId = 1 },
                new City { Id = 11, Name = "Bulandshahr", StateId = 1 },
                new City { Id = 12, Name = "Chandausi", StateId = 1 },
                new City { Id = 13, Name = "Deoria", StateId = 1 },
                new City { Id = 14, Name = "Etah", StateId = 1 },
                new City { Id = 15, Name = "Etawah", StateId = 1 },
                new City { Id = 16, Name = "Farrukhabad", StateId = 1 },
                new City { Id = 17, Name = "Fatehpur", StateId = 1 },
                new City { Id = 18, Name = "Firozabad", StateId = 1 },
                new City { Id = 19, Name = "Ghaziabad", StateId = 1 },
                new City { Id = 20, Name = "Gonda", StateId = 1 },



                new City { Id = 21, Name = "Lucknow", StateId = 1 },
                new City { Id = 22, Name = "Modinagar", StateId = 1 },
                new City { Id = 23, Name = "Mirzapur", StateId = 1 },
                new City { Id = 24, Name = "Meerut", StateId = 1 },
                new City { Id = 25, Name = "Maunath Bhajan(Mau)", StateId = 1 },
                new City { Id = 26, Name = "Mathura", StateId = 1 },
                new City { Id = 27, Name = "Mainpuri", StateId = 1 },
                new City { Id = 28, Name = "Lalitpur", StateId = 1 },
                new City { Id = 29, Name = "Lakhimpur", StateId = 1 },
                new City { Id = 30, Name = "Khurja", StateId = 1 },
                new City { Id = 31, Name = "Khora", StateId = 1 },
                new City { Id = 32, Name = "Kanpur Cantonment", StateId = 1 },
                new City { Id = 33, Name = "Kanpur", StateId = 1 },
                new City { Id = 34, Name = "Jhansi", StateId = 1 },
                new City { Id = 35, Name = "Jaunpur", StateId = 1 },
                new City { Id = 36, Name = "Hathras", StateId = 1 },
                new City { Id = 37, Name = "Hardoi", StateId = 1 },
                new City { Id = 38, Name = "Hapur", StateId = 1 },
                new City { Id = 39, Name = "Greater Noida", StateId = 1 },
                new City { Id = 40, Name = "Gorakhpur", StateId = 1 },



               new City { Id = 41, Name = "Moradabad", StateId = 1 },
                new City { Id = 42, Name = "Muzaffarnagar", StateId = 1 },
                new City { Id = 43, Name = "Noida", StateId = 1 },
                new City { Id = 44, Name = "Orai", StateId = 1 },
                new City { Id = 45, Name = "Pilibhit", StateId = 1 },
                new City { Id = 46, Name = "Prayagraj(Allahabad)", StateId = 1 },
                new City { Id = 47, Name = "Raebareli", StateId = 1 },
                new City { Id = 48, Name = "Rampur", StateId = 1 },
                new City { Id = 49, Name = "Saharanpur", StateId = 1 },
                new City { Id = 50, Name = "Sambhal", StateId = 1 },
                new City { Id = 51, Name = "Vrindavan", StateId = 1 },
                new City { Id = 52, Name = "Unnao", StateId = 1 },
                new City { Id = 53, Name = "Sultanpur", StateId = 1 },
                new City { Id = 54, Name = "Varanasi", StateId = 1 },
                new City { Id = 55, Name = "Sitapur", StateId = 1 },
                new City { Id = 56, Name = "Shikohabad", StateId = 1 },
                new City { Id = 57, Name = "Shamli", StateId = 1 },
                new City { Id = 58, Name = "Shahjahanpur", StateId = 1 },
                new City { Id = 59, Name = "Sarnath", StateId = 1 },
                new City { Id = 60, Name = "Sambhal", StateId = 1 },

                // Maharashtra Cities
                new City { Id = 64, Name = "Mumbai", StateId = 2 },
                new City { Id = 65, Name = "Pune", StateId = 2 },
                new City { Id = 66, Name = "Nagpur", StateId = 2 },
                new City { Id = 67, Name = "Nashik", StateId = 2 },
                new City { Id = 68, Name = "Aurangabad", StateId = 2 },

                // Delhi Cities
                new City { Id = 69, Name = "New Delhi", StateId = 3 },
                new City { Id = 70, Name = "Delhi Cantonment", StateId = 3 },

                // Karnataka Cities
                new City { Id = 71, Name = "Bangalore", StateId = 4 },
                new City { Id = 72, Name = "Mysore", StateId = 4 },
                new City { Id = 73, Name = "Mangalore", StateId = 4 },

                // West Bengal Cities
                new City { Id = 74, Name = "Kolkata", StateId = 5 },
                new City { Id = 75, Name = "Howrah", StateId = 5 },
                new City { Id = 76, Name = "Durgapur", StateId = 5 }
            );
            
        }
    }
}