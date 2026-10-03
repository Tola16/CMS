using CMS.Dtos.Customer;
using CMS.IUnitOfWorks.Interface;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _unitOfWork.CustomerRepo.GetAll().Select(customer => new
            {
                customer.Id,
                customer.FullName,
                customer.Email,
                customer.PhoneNumber
            });

            return Ok(customers);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _unitOfWork.CustomerRepo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            var a = new
            {
                customer.Id,
                customer.FullName,
                customer.Email,
                customer.PhoneNumber
            };

            return Ok(a);
        }

        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerDto customer)
        {
            var newCustomer = new Customer
            {
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
            _unitOfWork.CustomerRepo.Create(newCustomer);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CreateCustomerDto updatedCustomer)
        {
            var customer = _unitOfWork.CustomerRepo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.FullName = updatedCustomer.FullName;
            customer.Email = updatedCustomer.Email;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;

            _unitOfWork.CustomerRepo.Update(customer);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _unitOfWork.CustomerRepo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _unitOfWork.CustomerRepo.Delete(customer);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpGet("WithBookings/{id}")]
        public IActionResult GetAllCustomersWithBookings(int id)
        {
            var customers = _unitOfWork.CustomerRepo.GetAllCustomerWithBookings(id).Select(c => new
            {
                c.Id,
                c.FullName,
                c.Email,
                c.PhoneNumber,
                Bookings = c.Bookings.Select(b => new
                {
                    b.Id,
                    b.BookingDate,
                    b.TotalPrice,
                    b.NumberOfTickets

                }).ToList()
            });
            return Ok(customers);
        }

        [HttpGet("HighestSpenders")]
        public IActionResult GetHighestSpenders()
        {
            var customers = _unitOfWork.CustomerRepo.HighestSpenders().Select(c => new
            {
                c.Id,
                c.FullName,
                c.Email,
                c.PhoneNumber,
                TotalSpent = c.Bookings.Sum(b => b.TotalPrice)
            });
            return Ok(customers);
        }

        [HttpGet("NoBooking")]
        public IActionResult GetCustomerWithNoBooking()
        {
            var customer = _unitOfWork.CustomerRepo.NoBooking();
            if (customer == null)
            {
                return NotFound();
            }
            var a = new
            {
                customer.Id,
                customer.FullName,
                customer.Email,
                customer.PhoneNumber
            };
            return Ok(a);
        }


    }
}
