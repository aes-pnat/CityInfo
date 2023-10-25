using CityInfo.API.Entities;
using CityInfo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City("New York City")
                    {
                        Id = 1,
                        Description = "Rats and subways"

                    },
                    new City("Antwerp")
                    {
                        Id= 2,
                        Description = "Where ants like to werp"
                    },
                    new City("Paris")
                    {
                        Id = 3,
                        Description = "Baguette"
                    });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest("Central Park")
                    {
                        Id = 1,
                        CityId = 1,
                        Description = "Sticky stairs"
                    },
                    new PointOfInterest("Chrysler Building")
                    {
                        Id = 2,
                        CityId = 1,
                        Description = "Batman or something"
                    }, 
                    new PointOfInterest("Cathedral of Our Lady")
                    {
                        Id = 3,
                        CityId = 2,
                        Description = "Churthedral"
                    },
                    new PointOfInterest("Antwerp Central Station")
                    {
                        Id = 4,
                        CityId = 2,
                        Description = "Don't they only use bikes in Belgium"
                    },
                    new PointOfInterest("La Tour Eiffel")
                    {
                        Id = 5,
                        CityId = 3,
                        Description = "That's the thing from John Wick 4 right?"
                    },
                    new PointOfInterest("Les Champs-Elysees")
                    {
                        Id = 6,
                        CityId = 3,
                        Description = "Crashed the '71 Cuda here"
                    }
                    );

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
