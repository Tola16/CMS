using Microsoft.AspNetCore.Razor.Hosting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } 

        [Required, StringLength(100)]
        public string Director { get; set; } 

        [Range(30, 300)]
        public int Duration { get; set; }

        [Range(1950, 2026)]
        public int ReleaseYear { get; set; }

        [Range( 0 ,10)]
        public decimal Rating { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal TicketPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableSeats { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
