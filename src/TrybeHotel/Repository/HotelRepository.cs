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
            var hotelsList = _context.Hotels.ToList();
            var hotelsDtoList = new List<HotelDto>();

            foreach (var hotel in hotelsList)
            {
                hotelsDtoList.Add(new HotelDto
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    City = new CityDto
                    {
                        CityId = hotel.City.CityId,
                        Name = hotel.City.Name
                    }
                });
            }

            return hotelsDtoList;
        }

        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            return new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                City = new CityDto
                {
                    CityId = hotel.City.CityId,
                    Name = hotel.City.Name
                }
            };
        }
    }
}