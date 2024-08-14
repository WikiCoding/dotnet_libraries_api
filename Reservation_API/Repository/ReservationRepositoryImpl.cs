using Microsoft.EntityFrameworkCore;
using Reservation_API.Model;

namespace Reservation_API.Repository
{
    public class ReservationRepositoryImpl : IReservationRepository
    {
        private readonly ReservationDbContext _context;

        public ReservationRepositoryImpl(ReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsFromBookAsync(string bookId)
        {
            return await _context.Reservations.Where(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<int> SaveReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            
            return await _context.SaveChangesAsync();
        }
    }
}
