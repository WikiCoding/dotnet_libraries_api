namespace Books_API.Dto
{
    public class ReservationReq
    {
        public DateTime StartReservationDate { get; set; } 
        public DateTime EndReservationDate { get; set; }
        public string? UserDetails { get; set; }
    }
}
