using Reservation_API.Model;

namespace Reservation_API.Repository
{
    public interface IReservationRepository
    {
        Task<int> SaveReservationAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetAllReservationAsync();
        Task<IEnumerable<Reservation>> GetAllReservationsFromBookAsync(string bookId);
    }
}
