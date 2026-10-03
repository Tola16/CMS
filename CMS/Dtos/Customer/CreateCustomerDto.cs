using System.ComponentModel.DataAnnotations;

namespace CMS.Dtos.Customer
{
    public class CreateCustomerDto
    {
        
        public string FullName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
