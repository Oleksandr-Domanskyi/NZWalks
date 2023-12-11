using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions):base(dbContextOptions)   
        {
               
        }

        public DbSet<Difficulty>Difficulties { get; set; }
        public DbSet<Region>Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("f65d5aaa-307d-4e30-b129-d33104cf4aa6"),
                    Name="Easy"
                },
                new Difficulty()
                {
                    Id=Guid.Parse("58dc1430-6932-4636-9458-967a3a634b6c"),
                    Name="Medium"
                },
                new Difficulty()
                {
                    Id=Guid.Parse("8c6e5452-83e2-44a9-97a6-4f73576b1c9b"),
                    Name="Hard"
                }
            };
            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
             new Region()
               {
                   Id=Guid.Parse("aed57327-1524-42ea-b48e-9485e97cf983"),
                   Name = "Auckland",
                   Code="AKL",
                   ReoginImageURL="https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg/1024px-Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg"

               },
              new Region()
               {
                   Id=Guid.Parse("2dd0bceb-3795-4a47-92e1-f5bcef87d362"),
                   Name = "Zikland",
                   Code="ZKL",
                   ReoginImageURL="https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg/1024px-Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg"

               },
            };
            modelBuilder.Entity<Region>().HasData(regions); 
        }
    }
}
