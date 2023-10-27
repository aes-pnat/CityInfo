using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ICityInfoRepository _cityInfoRepository;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, 
            IMailService mailService,
            IMapper mapper,
            ICityInfoRepository cityInfoRepository) 
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? 
                throw new ArgumentNullException(nameof(mailService));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
            _cityInfoRepository = cityInfoRepository ?? 
                throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));


            //try
            //{
            //    // throw new Exception("Exception sample.");

            //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //    if (city == null)
            //    {
            //        _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
            //        return NotFound();
            //    }
            //    return Ok(city.PointsOfInterest);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
            //    return StatusCode(500, "A problem happened while handling your request.");
            //}

            
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId) 
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing the point of interest.");
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                _logger.LogInformation($"POI with id {pointOfInterestId} wasn't found.");
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterest = city.PointsOfInterest.FirstOrDefault(c=>c.Id == pointOfInterestId);
            //if(pointOfInterest == null)
            //{
            //    return NotFound();
            //}

            //return Ok(pointOfInterest);
        }


        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(
                cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn =
                _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var maxPointOfInterestID = _citiesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            //var finalPointOfInterest = new PointOfInterestDto()
            //{
            //    Id = ++maxPointOfInterestID,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description,
            //};
            //city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                    new
                    {
                        cityId = cityId,
                        pointOfInterestId = createdPointOfInterestToReturn.Id
                    },
                    createdPointOfInterestToReturn);
        }

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId,pointOfInterestId);
            if(pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();

            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //pointOfInterestFromStore.Name = pointOfInterest.Name;
            //pointOfInterestFromStore.Description = pointOfInterest.Description;

            //return NoContent();
        }

        //[HttpPatch("{pointofinterestid}")]
        //public ActionResult PartiallyUpdatePointOfInterest(
        //    int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterestForPatch = 
        //        new PointOfInterestForUpdateDto() 
        //        { 
        //            Name = pointOfInterestFromStore.Name,
        //            Description = pointOfInterestFromStore.Description
        //        };

        //    patchDocument.ApplyTo(pointOfInterestForPatch, ModelState);

        //    if (!ModelState.IsValid){
        //        return BadRequest(ModelState);    
        //    }

        //    if (!TryValidateModel(pointOfInterestForPatch))
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    pointOfInterestFromStore.Name = pointOfInterestForPatch.Name;
        //    pointOfInterestFromStore.Description = pointOfInterestForPatch.Description;

        //    return NoContent();

        //}

        //[HttpDelete("{pointofinterestid}")]
        //public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId) 
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    city.PointsOfInterest.Remove(pointOfInterestFromStore);
        //    _mailService.Send("Point of interest deleted", $"Deleted point of interest with id {pointOfInterestFromStore.Id} from city {city.Id}");
        //    return NoContent(); 
        //}


    }
}
