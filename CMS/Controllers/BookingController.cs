using CMS.Dtos.Booking;
using CMS.IUnitOfWorks.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _unitOfWork.BookingRepo.GetAll().Select(booking => new
            {
                booking.Id,
                booking.BookingDate,
                booking.NumberOfTickets,
                booking.TotalPrice,
                booking.CustomerId,
                booking.MovieId
            });

            return Ok(bookings);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBookingById(int id)
        {
            var booking = _unitOfWork.BookingRepo.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                booking.Id,
                booking.BookingDate,
                booking.NumberOfTickets,
                booking.TotalPrice,
                booking.CustomerId,
                booking.MovieId
            });
        }

        [HttpPost("book")]
        public IActionResult BookMovie(BookMovieDto request)
        {
            var customer = _unitOfWork.CustomerRepo.GetById(request.CustomerId);
            if (customer == null)
            {
                return NotFound( "Customer not found." );
            }

            var movie = _unitOfWork.MovieRepo.GetById(request.MovieId);
            if (movie == null)
            {
                return NotFound("Movie not found." );
            }

            if (request.NumberOfTickets > movie.AvailableSeats)
            {
                return BadRequest( "The requested number of tickets exceeds the available seats." );
            }

            var booking = _unitOfWork.BookingRepo.BookMovie(customer, movie, request.NumberOfTickets);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, new
            {
                booking.Id,
                booking.BookingDate,
                booking.NumberOfTickets,
                booking.TotalPrice,
                booking.CustomerId,
                booking.MovieId
            });
        }

        [HttpPut("cancel/{id}")]
        public IActionResult CancelBooking(int id)
        {
            if (!_unitOfWork.BookingRepo.CancelBooking(id))
            {
                return NotFound();
            }

            _unitOfWork.Save();
            return NoContent();
        }

        [HttpGet("details")]
        public IActionResult GetBookingDetails()
        {
            var bookingDetails = _unitOfWork.BookingRepo.GetBookingDetails()
                .Select(booking => new
                {
                    booking.Id,
                    CustomerName = booking.Customer.FullName,
                    MovieTitle = booking.Movie.Title,
                    GenreName = booking.Movie.Genre.Name,
                    booking.BookingDate,
                    booking.NumberOfTickets,
                    booking.TotalPrice
                });

            return Ok(bookingDetails);
        }

        [HttpGet("revenue")]
        public IActionResult GetRevenue()
        {
            var totalRevenue = _unitOfWork.BookingRepo.GetTotalRevenue();
            return Ok(new { TotalRevenue = totalRevenue });
        }
    }
}
