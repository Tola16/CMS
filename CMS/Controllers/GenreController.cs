using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using CMS.IUnitOfWorks.Interface;
using CMS.Models;
using CMS.Dtos.Genre;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var genres = _unitOfWork.GenreRepo.GetAll().Select(
                a => new
                {
                    a.Id,
                    a.Name
                }
                );
            return Ok(genres);
        }


        [HttpGet("GetById/{id}")]
        public IActionResult GetGenreById(int id)
        {
            var genre = _unitOfWork.GenreRepo.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }

            var newgenre = new
            {
                genre.Id,
                genre.Name
            };




            return Ok(newgenre);
        }

        [HttpPost]
        public IActionResult CreateGenre(CreateGenreDto dto)
        {
            var a = new Genre
            {
                Name = dto.Name
            };
            _unitOfWork.GenreRepo.Create(a);
            _unitOfWork.Save();
            return Ok(a);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, CreateGenreDto dto)
        {
            var genre = _unitOfWork.GenreRepo.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            genre.Name = dto.Name;
            _unitOfWork.GenreRepo.Update(genre);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var genre = _unitOfWork.GenreRepo.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            _unitOfWork.GenreRepo.Delete(genre);
            _unitOfWork.Save();
            return NoContent();
        }


        [HttpGet("GetAllWithMovies")]
        public IActionResult GetAllGenresWithMovies()
        {
            var genres = _unitOfWork.GenreRepo.GetAllGenreWithMovies().Select(
                a => new
                {
                    a.Id,
                    a.Name,
                    Movies = a.Movies.Select(m => new
                    {
                        m.Id,
                        m.Title,
                        m.Director,
                        m.Duration,
                        m.ReleaseYear,
                        m.Rating,
                        m.TicketPrice,
                        m.AvailableSeats
                    })
                }
                );
            return Ok(genres);
        }





    }

}