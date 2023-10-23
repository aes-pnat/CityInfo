using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;
        public CitiesController(CitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetAllCities()
        {
            return Ok(_citiesDataStore.Cities);
        }

        [HttpGet("{id}", Name = "GetCity")]
        public IActionResult GetCity(int id)
        {
            var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }

        [HttpPost]
        public ActionResult<CityDto> CreateCity(CityForCreationDto city)
        {
            var maxCityID = _citiesDataStore.Cities.Max(c => c.Id);

            var finalCity = new CityDto()
            {
                Id = ++maxCityID,
                Name = city.Name,
                Description = city.Description,
            };
            _citiesDataStore.Cities.Add(finalCity);

            return CreatedAtRoute("GetCity",
                    new
                    {
                        id = finalCity.Id
                    },
                    finalCity); 
        }
    }
}
