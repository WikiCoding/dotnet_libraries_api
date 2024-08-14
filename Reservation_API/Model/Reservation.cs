using System.ComponentModel.DataAnnotations;

namespace Reservation_API.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? BookId { get; set; }
        [Required]
        public DateTime? StartReservationDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndReservationDate { get; set; }
        [Required]
        public string? UserDetails { get; set; }
    }
}
