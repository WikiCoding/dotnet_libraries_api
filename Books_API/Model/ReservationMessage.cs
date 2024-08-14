namespace Books_API.Model
{
    public class ReservationMessage
    {
        public string BookId { get; set; }
        public DateTime StartReservationDate { get; set; }
        public DateTime EndReservationDate { get; set; }
        public string? UserDetails { get; set; }
    }
}
