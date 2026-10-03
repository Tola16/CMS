using CMS.AppContext;
using CMS.Models;
using CMS.Repos.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repos.Implementation
{
    public class MovieRepo : GenericRepo<Movie>, IMovieRepo
    {
        private readonly AppDbContext _context;
        public MovieRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }
        public Movie SearchWithTheTitle(string title)
        {
            return _context.Movies.FirstOrDefault(m => m.Title == title);

        }
        public ICollection<Movie> RatingGreaterThan(int rating)
        {
            return _context.Movies.Where(m => m.Rating > rating).ToList();
        }

        public ICollection<Movie> GetAllWithRange(int minRating, int maxRating)
        {
            return _context.Movies.Where(m => m.Rating >= minRating && m.Rating <= maxRating).ToList();
        }
        public ICollection<Movie> AvailableSeats()
        {
            return _context.Movies.Where(m => m.AvailableSeats > 0).ToList();
        }

        public ICollection<Movie> TopRated()
        {
            var topRatedMovies = _context.Movies.OrderByDescending(m => m.Rating).Take(5).ToList();

            return topRatedMovies;
        }

        public ICollection<Movie> Latest()
        {
            var a = _context.Movies.OrderByDescending(m => m.ReleaseYear).Take(1).ToList();
            return a;
        }

        public Movie HighestBooking()
        {
            return _context.Movies
                .OrderByDescending(m => m.Bookings.Count)
                .FirstOrDefault();

        }

        public int TotalNumberOfMovies()
        {
            return _context.Movies.Count();
        }

        public decimal AveragePriceOfAllMovies()
        {
            return _context.Movies.Average(m => m.TicketPrice);
        }
        
        public ICollection<Movie> GetAllWithGenre(int id)
        {
            return _context.Movies.Include(m => m.Genre).Where(m => m.GenreId == id).ToList();
        }

      

    }
}