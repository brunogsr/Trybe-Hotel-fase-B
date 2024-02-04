using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            var hotels = _context.Hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Address = h.Address,
                CityId = h.CityId,
                CityName = h.City!.Name,
            });
            return hotels;
        }

        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
            return new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                CityId = hotel.CityId,
                CityName = city!.Name
            };
        }
    }
}