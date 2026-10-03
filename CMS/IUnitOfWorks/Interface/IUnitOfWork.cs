using CMS.Repos.Interface;

namespace CMS.IUnitOfWorks.Interface
{
    public interface IUnitOfWork
    {
        public IBookingRepo BookingRepo { get; }
        public IGenreRepo GenreRepo { get; }
        public IMovieRepo MovieRepo { get; }
        public ICustomerRepo CustomerRepo { get; }
        public void Save();
    }
}
