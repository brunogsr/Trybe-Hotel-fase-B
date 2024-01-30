namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 1. Implemente as models da aplicação
public class Booking
{
  [Key]
  public int BookingId { get; set; }
  public DateTime CheckIn { get; set; }
  public DateTime CheckOut { get; set; }
  public int GuestQuant { get; set; }
  [ForeignKey("UserId")]
  public User? UserId { get; set; }
  [ForeignKey("RoomId")]
  public Room? RoomId { get; set; }

}