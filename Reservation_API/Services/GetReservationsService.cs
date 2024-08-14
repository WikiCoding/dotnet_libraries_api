using Reservation_API.Model;
using Reservation_API.Repository;

namespace Reservation_API.Services
{
    public class GetReservationsService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<GetReservationsService> _logger;

        public GetReservationsService(IReservationRepository reservationRepository, ILogger<GetReservationsService> logger)
        {
            _reservationRepository = reservationRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationRepository.GetAllReservationAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsByBook(string id)
        {
            return await _reservationRepository.GetAllReservationsFromBookAsync(id);
        }
    }
}
