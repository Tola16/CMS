using System.ComponentModel.DataAnnotations;

namespace CMS.Dtos.Booking
{
    public class CreateBookingDto
    {
        public DateTime? BookingDate { get; set; }

        public int NumberOfTickets { get; set; }

        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public int MovieId { get; set; }

    }
}
