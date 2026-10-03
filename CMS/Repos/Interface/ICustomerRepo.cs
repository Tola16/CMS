using CMS.Models;

namespace CMS.Repos.Interface
{
    public interface ICustomerRepo : IGenericRepo<Customer>
    {
        public ICollection<Customer> GetAllCustomerWithBookings(int id );
        public ICollection<Customer> HighestSpenders();
        public Customer NoBooking();

    }
}
