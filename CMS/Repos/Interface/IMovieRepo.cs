using CMS.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CMS.Repos.Interface
{
    public interface IMovieRepo : IGenericRepo<Movie>
    {
        public Movie SearchWithTheTitle(string title);
        public ICollection<Movie> RatingGreaterThan(int rating);
        public ICollection<Movie> GetAllWithRange(int minRating, int maxRating);
        public ICollection<Movie> AvailableSeats();
        public ICollection<Movie> TopRated();
        public ICollection<Movie> Latest();
        public Movie HighestBooking();
        public int TotalNumberOfMovies();
        public decimal AveragePriceOfAllMovies();
        public ICollection<Movie> GetAllWithGenre(int id);


    }
}
