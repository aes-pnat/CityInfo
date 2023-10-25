using CityInfo.API.DbContexts;
using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            
        }
        public Task<IEnumerable<City>> GetCitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<City?> GetCityAsync(int cityId)
        {
            throw new NotImplementedException();
        }

        public Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            throw new NotImplementedException();
        }
    }
}
