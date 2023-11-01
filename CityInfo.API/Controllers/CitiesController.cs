using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(
            [FromQuery(Name = "name")] string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }
            var (cityEntities, paginationMetadata) = await _cityInfoRepository
                .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}", Name = "GetCity")]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
            if(city == null)
            {
                return NotFound();
            }
            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }
            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));

            //var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

            //if (cityToReturn == null)
            //{
            //    return NotFound();
            //}
            //return Ok(cityToReturn);
        }

        //[HttpPost]
        //public ActionResult<CityDto> CreateCity(CityForCreationDto city)
        //{
        //    var maxCityID = _citiesDataStore.Cities.Max(c => c.Id);

        //    var finalCity = new CityDto()
        //    {
        //        Id = ++maxCityID,
        //        Name = city.Name,
        //        Description = city.Description,
        //    };
        //    _citiesDataStore.Cities.Add(finalCity);

        //    return CreatedAtRoute("GetCity",
        //            new
        //            {
        //                id = finalCity.Id
        //            },
        //            finalCity); 
        //}
    }
}
