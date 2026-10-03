using CMS.Models;

namespace CMS.Repos.Interface
{
    public interface IBookingRepo : IGenericRepo<Booking>
    {
        Booking BookMovie(Customer customer, Movie movie, int numberOfTickets);
        bool CancelBooking(int id);
        ICollection<Booking> GetBookingDetails();
        decimal GetTotalRevenue();
    }
}
