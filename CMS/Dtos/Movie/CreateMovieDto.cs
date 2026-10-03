using System.ComponentModel.DataAnnotations;

namespace CMS.Dtos.Movie
{
    public class CreateMovieDto
    {
        public string Title { get; set; }

        public string Director { get; set; }

        public int Duration { get; set; }

        public int ReleaseYear { get; set; }

        public decimal Rating { get; set; }

        public decimal TicketPrice { get; set; }

        public int AvailableSeats { get; set; }

        public int GenreId { get; set; }
    }
}
