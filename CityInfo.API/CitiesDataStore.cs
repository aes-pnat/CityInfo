using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        // public static CitiesDataStore Current { get; set; } = new CitiesDataStore();

        public CitiesDataStore() 
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "Rats and subways",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description="Sticky stairs"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Chrysler Building",
                            Description = "Batman or something"
                        }
                    }

                },
                new CityDto()
                {
                    Id= 2,
                    Name = "Antwerp",
                    Description = "Where ants like to werp",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Cathedral of Our Lady",
                            Description="Churthedral"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Antwerp Central Station",
                            Description = "Don't they only use bikes in Belgium"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "Baguette",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "La Tour Eiffel",
                            Description="That's the thing from John Wick 4 right?"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "Les Champs-Elysees",
                            Description = "Crashed the '71 Cuda here"
                        }
                    }
                }
            };
        }
    }
}
