using CMS.AppContext;
using CMS.Models;
using CMS.Repos.Interface;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repos.Implementation
{
    public class BookingRepo : GenericRepo<Booking>, IBookingRepo
    {
        public BookingRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Booking BookMovie(Customer customer, Movie movie, int numberOfTickets)
        {
            movie.AvailableSeats -= numberOfTickets;

            var booking = new Booking
            {
                BookingDate = DateTime.UtcNow,
                NumberOfTickets = numberOfTickets,
                TotalPrice = movie.TicketPrice * numberOfTickets,
                CustomerId = customer.Id,
                MovieId = movie.Id
            };

            _context.Bookings.Add(booking);
            return booking;
        }

        public bool CancelBooking(int id)
        {
            var booking = _context.Bookings
                .Include(item => item.Movie)
                .FirstOrDefault(item => item.Id == id);

            if (booking == null)
            {
                return false;
            }

            booking.Movie.AvailableSeats += booking.NumberOfTickets;
            _context.Bookings.Remove(booking);
            return true;
        }

        public ICollection<Booking> GetBookingDetails()
        {
            return _context.Bookings
                .Include(booking => booking.Customer)
                .Include(booking => booking.Movie)
                    .ThenInclude(movie => movie.Genre)
                .ToList();
        }

        public decimal GetTotalRevenue()
        {
            return _context.Bookings.Sum(booking => (decimal?)booking.TotalPrice) ?? 0m;
        }
    }
}
