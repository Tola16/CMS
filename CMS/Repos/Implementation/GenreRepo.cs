using CMS.AppContext;
using CMS.Models;
using CMS.Repos.Interface;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repos.Implementation
{
    public class GenreRepo : GenericRepo<Genre>, IGenreRepo
    {
        readonly AppDbContext _context;
        public GenreRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }
        public ICollection<Genre> GetAllGenreWithMovies()
        {
            return _context.Genres.Include(a => a.Movies).ToList();

        }


    }
}
