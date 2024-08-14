namespace Reservation_API.Dto
{
    public class ReservationReq
    {
        public string? BookId { get; set; }
        public DateTime StartReservationDate { get; set; }
        public DateTime EndReservationDate { get; set; }
        public string? UserDetails { get; set; }

        public ReservationReq(string? libraryId, string? bookId, DateTime startReservationDate, DateTime endReservationDate, string? userDetails)
        {
            if (EndReservationDate < StartReservationDate)
            {
                throw new FormatException("End Date cannot be after Start Date");
            }
            
            BookId = bookId;
            StartReservationDate = startReservationDate;
            EndReservationDate = endReservationDate;
            UserDetails = userDetails;
        }
    }
}
