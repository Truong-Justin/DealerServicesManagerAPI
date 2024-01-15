using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DealerServicesManagerAPI.Controllers
{
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly DealerServicesDbContext _context;

        public EmployeeController(DealerServicesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<ActionResult> GetEmployeeById(int employeeId)
        {
            Employee employee = await _context.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                return NotFound("An employee with the given Id doesn't exist");
            }

            return Ok(employee);
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<ActionResult> AddEmployee(string firstName, string lastName, string address, string zip, string state, string city, string phone, int dealerId)
        {
            await _context.Employees.AddAsync(new Employee()
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

            return Ok("A new employee has been added");
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployee(int employeeId, string address, string zip, string state, string city, string phone, int dealerId)
        {
            Employee employee = await _context.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync<Employee>();

            if (employee != null)
            {
                employee.Address = address;
                employee.Zip = zip;
                employee.State = state;
                employee.City = city;
                employee.Phone = phone;
                employee.DealerId = dealerId;

                await _context.SaveChangesAsync();
            }

            else
            {
                return NotFound("An employee with the given Id doesn't exist");
            }

            return Ok("The employee has been updated.");
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            Employee employee = await _context.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                return NotFound("An employee with the given Id doesn't exist");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok("The employee has been deleted");
        }
    }
}

