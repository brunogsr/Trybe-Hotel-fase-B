using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<CityDto> GetCities()
        {
            var citiesList = _context.Cities.ToList();
            var citiesDtoList = new List<CityDto>();
            foreach (var city in citiesList)
            {
                citiesDtoList.Add(new CityDto
                {
                    CityId = city.CityId,
                    Name = city.Name
                });
            }
            return citiesDtoList;
        }

        public CityDto AddCity(City city)
        {
            var newCity = _context.Cities.Add(city);
            _context.SaveChanges();
            return new CityDto
            {
                CityId = newCity.Entity.CityId,
                Name = newCity.Entity.Name
            };
        }

    }
}