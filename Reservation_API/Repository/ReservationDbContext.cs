using Microsoft.EntityFrameworkCore;
using Reservation_API.Model;

namespace Reservation_API.Repository
{
    public class ReservationDbContext(DbContextOptions<ReservationDbContext> options) : DbContext(options)
    {
        public DbSet<Reservation> Reservations { get; set; }
    }
}
