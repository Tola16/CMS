using CMS.Models;

namespace CMS.Repos.Interface
{
    public interface IGenreRepo : IGenericRepo<Genre>
    {
        public ICollection<Genre> GetAllGenreWithMovies();
    }
}
