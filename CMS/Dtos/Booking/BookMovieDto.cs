using System.ComponentModel.DataAnnotations;

namespace CMS.Dtos.Booking
{
    public class BookMovieDto
    {
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }

        [Range(1, int.MaxValue)]
        public int MovieId { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfTickets { get; set; }
    }
}
