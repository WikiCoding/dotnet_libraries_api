using Microsoft.AspNetCore.Mvc;
using Reservation_API.Dto;
using Reservation_API.Model;
using Reservation_API.Services;

namespace Reservation_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly GetReservationsService _getReservationsService;

        public ReservationsController(GetReservationsService getReservationsService)
        {
            _getReservationsService = getReservationsService;
        }

        [HttpGet("{book-id}")]
        public async Task<IActionResult> GetAllReservationsFromBook([FromRoute(Name = "book-id")] string _id)
        {
            IEnumerable<Reservation> reservations = await _getReservationsService.GetAllReservationsByBook(_id);
            IEnumerable<ReservationRes> response = MapReservationToResponse(reservations);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _getReservationsService.GetAllReservations();

            var response = MapReservationToResponse(reservations);

            return Ok(response);
        }

        private static IEnumerable<ReservationRes> MapReservationToResponse(IEnumerable<Reservation> reservations)
        {
            return reservations.ToList().ConvertAll(
                            r => new ReservationRes(r.Id, r.BookId, r.StartReservationDate, r.EndReservationDate, r.UserDetails));
        }
    }
}
