using CMS.AppContext;
using CMS.Models;
using CMS.Repos.Interface;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repos.Implementation
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        private readonly AppDbContext _context;
        public CustomerRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public ICollection<Customer> GetAllCustomerWithBookings(int id)
        {
            return _context.Customers.Include(c => c.Bookings).Where(c => c.Id == id).ToList();
        }
        public ICollection<Customer> HighestSpenders()
        {
            return _context.Customers.Include(c => c.Bookings)
                .OrderByDescending(c => c.Bookings.Sum(b => b.TotalPrice))
                .ToList();
        }

        public Customer NoBooking()
        {
            return _context.Customers.Include(c => c.Bookings).Where(c => c.Bookings.Count == 0).FirstOrDefault();
        }
    }
}
