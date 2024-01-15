using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DealerServicesManagerAPI.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DealerServicesDbContext _context;

        public CustomerController(DealerServicesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<ActionResult> GetCustomerById(int customerId)
        {
            Customer customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound("A customer with the given Id doesn't exist");
            }

            return Ok(customer);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult> AddCustomer(string firstName, string lastName, string address, string zip, string state, string city, string phone, int dealerId)
        {
            await _context.Customers.AddAsync(new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Zip = zip,
                State = state,
                City = city,
                Phone = phone,
                DealerId = dealerId
            });

            await _context.SaveChangesAsync();

            return Ok("A new customer has been added.");
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<ActionResult> UpdateCustomer(int customerId, string address, string zip, string state, string city, string phone)
        {
            Customer customer = await _context.Customers.FindAsync(customerId);

            if (customer != null)
            {
                customer.Address = address;
                customer.Zip = zip;
                customer.State = state;
                customer.City = city;
                customer.Phone = phone;

                await _context.SaveChangesAsync();
            }

            else 
            {
                return NotFound("A customer with the given Id doesn't exist");
            }

            return Ok("The customer has been updated.");
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<ActionResult> DeleteCustomer(int customerId)
        {
            Customer customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound("A customer with the given Id doesn't exist");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("The customer has been deleted.");
        }

    }
}

