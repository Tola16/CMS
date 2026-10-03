namespace CMS.IUnitOfWorks.Implementations
{
    using CMS.AppContext;
    using CMS.IUnitOfWorks.Interface;
    using CMS.Repos.Interface;

    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IBookingRepo bookingRepo, IGenreRepo genreRepo, IMovieRepo movieRepo, ICustomerRepo customerRepo , AppDbContext appDbContext)
        {
            BookingRepo = bookingRepo;
            GenreRepo = genreRepo;
            MovieRepo = movieRepo;
            CustomerRepo = customerRepo;
            _context = appDbContext;
        }

        public IBookingRepo BookingRepo { get; }
        public IGenreRepo GenreRepo { get; }
        public IMovieRepo MovieRepo { get; }
        public ICustomerRepo CustomerRepo { get; }
        private readonly AppDbContext _context;

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
