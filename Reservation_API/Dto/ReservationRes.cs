using System.Net;

namespace Reservation_API.Dto
{
    public record ReservationRes(int Id, string BookId, DateTime? StartReservationDate, DateTime EndReservationDate, string UserDetails);
    
}
