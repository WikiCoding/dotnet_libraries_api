using Books_API.Dto;
using Books_API.Exceptions;
using Books_API.Model;
using Books_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly BooksReservationServiceProducer _reservationServiceProducer;

        public BooksController(BookService bookService, BooksReservationServiceProducer reservationServiceProducer)
        {
            _bookService = bookService;
            _reservationServiceProducer = reservationServiceProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookReq req)
        {
            if (!IsValidBookReq(req)) 
            {
                return BadRequest("Invalid inputs");
            }

            try
            {
                var bookDm = await _bookService.CreateBookService(req.Title, req.LibraryId);

                return CreatedAtAction(nameof(CreateBook), bookDm);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SerializerException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            IEnumerable<BookDataModel> books = await _bookService.GetAllBooksService();

            IEnumerable<BookRes> bookRes = books.ToList().ConvertAll(book => 
            new BookRes() { _id = book._id.ToString(), Title = book.Title, IsReserved = book.Reserved, LibraryId = book.LibraryId });

            return Ok(bookRes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute(Name = "id")] string _id)
        {
            if (_id.Trim().Length == 0)
            {
                return BadRequest("Invalid inputs");
            }

            try
            {
                BookDataModel bookDataModel = await _bookService.GetBookByIdService(_id);

                BookRes bookRes = 
                    new() { _id = bookDataModel._id.ToString(), Title = bookDataModel.Title, IsReserved = bookDataModel.Reserved, LibraryId = bookDataModel.LibraryId };

                return Ok(bookRes);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute(Name ="id")] string _id, [FromBody] BookReq req)
        {
            if (!IsValidBookReq(req) || _id.Trim().Length == 0)
            {
                return BadRequest("Invalid inputs");
            }

            try
            {
                BookDataModel bookDataModel = await _bookService.UpdateBookService(_id, req.Title, req.LibraryId);

                BookRes bookRes = 
                    new() { _id = bookDataModel._id.ToString(), Title = bookDataModel.Title, IsReserved = bookDataModel.Reserved, LibraryId = bookDataModel.LibraryId };

                return Ok(bookRes);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SerializerException ex)
            {
                return Problem(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute(Name = "id")] string _id)
        {
            if (_id.Trim().Length == 0)
            {
                return BadRequest("Invalid Id");
            }

            try
            {
                var bookDataModel = await _bookService.DeleteBookById(_id);

                BookRes bookRes = 
                    new() { _id = bookDataModel._id.ToString(), Title = bookDataModel.Title, IsReserved = bookDataModel.Reserved, LibraryId = bookDataModel.LibraryId };

                return Ok(bookRes);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reservations")]
        public async Task<IActionResult> CreateBookReservation([FromQuery(Name = "book-id")] string _id, 
            [FromBody] ReservationReq reservationReq)
        {
            if (reservationReq.EndReservationDate < reservationReq.StartReservationDate) return BadRequest("End Date cannot be before Start Date");
            if (reservationReq.UserDetails!.Trim().Length == 0) return BadRequest("User Details are required");

            try
            {
                await _reservationServiceProducer.CreateReservation(_id, reservationReq);

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool IsValidBookReq(BookReq req)
        {
            return req.Title.Trim().Length == 0 || req.LibraryId.Trim().Length == 0 ? false : true;
        }
    }
}
