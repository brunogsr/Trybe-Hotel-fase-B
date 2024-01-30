using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var rooms = from r in _context.Rooms
                        where r.HotelId == HotelId
                        select new RoomDto
                        {
                            RoomId = r.RoomId,
                            Name = r.Name,
                            Capacity = r.Capacity,
                            Image = r.Image,
                            Hotel = (from hotel in _context.Hotels
                                     where hotel.HotelId == HotelId
                                     select new HotelDto()
                                     {
                                         HotelId = hotel.HotelId,
                                         Name = hotel.Name,
                                         Address = hotel.Address,
                                         CityId = hotel.CityId,
                                         CityName = (from c in _context.Cities
                                                     where c.CityId == hotel.CityId
                                                     select c.Name).FirstOrDefault()
                                     }).FirstOrDefault()
                        };
            return rooms;
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return new RoomDto
            {
                RoomId = room.RoomId,
                Name = room.Name,
                Capacity = room.Capacity,
                Image = room.Image,
                HotelId = room.HotelId,
                Hotel = (from hotel in _context.Hotels
                         where hotel.HotelId == room.HotelId
                         select new HotelDto()
                         {
                             HotelId = hotel.HotelId,
                             Name = hotel.Name,
                             Address = hotel.Address,
                             CityId = hotel.CityId,
                             CityName = (from c in _context.Cities
                                         where c.CityId == hotel.CityId
                                         select c.Name).FirstOrDefault()
                         }).FirstOrDefault()
            };
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId)
        {
            var room = _context.Rooms.Find(RoomId);
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}