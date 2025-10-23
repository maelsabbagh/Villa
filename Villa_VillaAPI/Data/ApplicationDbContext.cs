using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Villa> Villas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createdTime = new DateTime(2025, 10, 23, 0, 0, 0, DateTimeKind.Local);

            modelBuilder.Entity<Villa>().HasData(
                 new Villa
                 {
                     Id = 1,
                     Name = "Royal Villa",
                     Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                     ImageURL = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                     Occupancy = 4,
                     Rate = 200,
                     Sqmt = 550,
                     Amenity = "",
                     CreatedDate = createdTime
                 },
              new Villa
              {
                  Id = 2,
                  Name = "Premium Pool Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageURL = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                  Occupancy = 4,
                  Rate = 300,
                  Sqmt = 550,
                  Amenity = "",
                  CreatedDate = createdTime
              },
              new Villa
              {
                  Id = 3,
                  Name = "Luxury Pool Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageURL = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
                  Occupancy = 4,
                  Rate = 400,
                  Sqmt = 750,
                  Amenity = "",
                  CreatedDate = createdTime
              },
              new Villa
              {
                  Id = 4,
                  Name = "Diamond Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageURL = "https://dotnetmastery.com/bluevillaimages/villa5.jpg",
                  Occupancy = 4,
                  Rate = 550,
                  Sqmt = 900,
                  Amenity = "",
                  CreatedDate = createdTime
              },
              new Villa
              {
                  Id = 5,
                  Name = "Diamond Pool Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageURL = "https://dotnetmastery.com/bluevillaimages/villa2.jpg",
                  Occupancy = 4,
                  Rate = 600,
                  Sqmt = 1100,
                  Amenity = "",
                  CreatedDate = createdTime
              });
        }
    }
}
