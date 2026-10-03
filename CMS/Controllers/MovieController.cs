using CMS.Dtos.Movie;
using CMS.IUnitOfWorks.Interface;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = _unitOfWork.MovieRepo.GetAll().Select(movie => new
            {
                movie.Id,
                movie.Title,
                movie.Director,
                movie.Duration,
                movie.ReleaseYear,
                movie.Rating,
                movie.TicketPrice,
                movie.AvailableSeats,
                movie.GenreId
            });

            return Ok(movies);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _unitOfWork.MovieRepo.GetById(id);
             
            var a = new
            {
                movie.Id,
                movie.Title,
                movie.Director,
                movie.Duration,
                movie.ReleaseYear,
                movie.Rating,
                movie.TicketPrice,
                movie.AvailableSeats,
                movie.GenreId
            };

            if (movie == null)
            {
                return NotFound();
            }


            return Ok(a);
        }

        [HttpPost]
        public IActionResult CreateMovie(CreateMovieDto movie)
        {
            var newMovie = new Movie
            {
                Title = movie.Title,
                Director = movie.Director,
                Duration = movie.Duration,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating,
                TicketPrice = movie.TicketPrice,
                AvailableSeats = movie.AvailableSeats,
                GenreId = movie.GenreId
            };

            _unitOfWork.MovieRepo.Create(newMovie);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetMovieById), new { id = newMovie.Id }, movie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, CreateMovieDto updatedMovie)
        {
            var movie = _unitOfWork.MovieRepo.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = updatedMovie.Title;
            movie.Director = updatedMovie.Director;
            movie.Duration = updatedMovie.Duration;
            movie.ReleaseYear = updatedMovie.ReleaseYear;
            movie.Rating = updatedMovie.Rating;
            movie.TicketPrice = updatedMovie.TicketPrice;
            movie.AvailableSeats = updatedMovie.AvailableSeats;
            movie.GenreId = updatedMovie.GenreId;

            _unitOfWork.MovieRepo.Update(movie);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _unitOfWork.MovieRepo.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            _unitOfWork.MovieRepo.Delete(movie);
            _unitOfWork.Save();
            return NoContent();
        }


        [HttpGet("Search")]
        public IActionResult Search(string title)
        {
            var movie = _unitOfWork.MovieRepo.SearchWithTheTitle(title);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("Rating")]
        public IActionResult GetMoviesWithRatingGreaterThan(int rating)
        {
            var movie = _unitOfWork.MovieRepo.RatingGreaterThan(rating);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("Range")]
        public IActionResult GetMoviesWithRatingRange(int minRating, int maxRating)
        {
            var movie = _unitOfWork.MovieRepo.GetAllWithRange(minRating, maxRating);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("AvailableSeats")]
        public IActionResult GetMoviesWithAvailableSeats()
        {
            var movie = _unitOfWork.MovieRepo.AvailableSeats();
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("Top5")]
        public IActionResult GetTopRatedMovies()
        {
            var topRatedMovies = _unitOfWork.MovieRepo.TopRated();
            return Ok(topRatedMovies);
        }


        [HttpGet("Latest")]
        public IActionResult GetLatestMovie()
        {
            var latestMovie = _unitOfWork.MovieRepo.Latest();
            return Ok(latestMovie);
        }

        [HttpGet("MostBooked")]
        public IActionResult GetMostBookedMovie()
        {
            var mostBookedMovie = _unitOfWork.MovieRepo.HighestBooking();
            return Ok(mostBookedMovie);
        }

        [HttpGet("TotalCount")]
        public IActionResult GetTotalNumberOfMovies()
        {
            var totalCount = _unitOfWork.MovieRepo.TotalNumberOfMovies();
            return Ok(totalCount);
        }

        [HttpGet("AveragePrice")]
        public IActionResult GetAveragePriceOfAllMovies()
        {
            var averagePrice = _unitOfWork.MovieRepo.AveragePriceOfAllMovies();
            return Ok(averagePrice);
        }

        [HttpGet("WithGenre")]

        public IActionResult GetAllMoviesWithGenre(int id)
        {
            var moviesWithGenre = _unitOfWork.MovieRepo.GetAllWithGenre(id).Select(m => new
            {
                m.Id,
                m.Title,
                m.ReleaseYear,
                m.Rating,
                m.TicketPrice,
                m.Genre.Name,
                


            });
            return Ok(moviesWithGenre);
        }




    }
}
