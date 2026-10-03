using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string FullName { get; set; } 

        [Required, EmailAddress]
        public string Email { get; set; } 

        public string? PhoneNumber { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
