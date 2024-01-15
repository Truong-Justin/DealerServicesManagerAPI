using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DealerServicesManagerAPI.Controllers
{
    [Route("api/Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly DealerServicesDbContext _context;

        public ServiceController(DealerServicesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllServices")]
        public async Task<ActionResult> GetAllServices()
        {
            return Ok(await _context.Services.ToListAsync());
        }

        [HttpGet]
        [Route("GetServiceById")]
        public async Task<ActionResult> GetServiceById(int serviceId)
        {
            Service service = await _context.Services.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound("A service with the given Id doesn't exist");
            }

            return Ok(service);
        }

        [HttpPost]
        [Route("AddService")]
        public async Task<ActionResult> AddService(string serviceName, int laborHours)
        {
            await _context.Services.AddAsync(new Service()
            {
                ServiceName = serviceName,
                LaborHours = laborHours
            });

            await _context.SaveChangesAsync();

            return Ok("A new service has been added.");
        }

        [HttpPut]
        [Route("UpdateService")]
        public async Task<ActionResult> UpdateService(int serviceId, int laborHours)
        {
            Service service = await _context.Services.Where(s => s.ServiceId == serviceId).FirstOrDefaultAsync<Service>();

            if (service != null)
            {
                service.LaborHours = laborHours;

                await _context.SaveChangesAsync();
            }

            else
            {
                return NotFound("A service with the given Id doesn't exist");
            }

            return Ok("The service has been updated.");
        }

        [HttpDelete]
        [Route("DeleteService")]
        public async Task<ActionResult> DeleteService(int serviceId)
        {
            Service service = await _context.Services.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound("A service with the given Id doesn't exist");
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok("The service has been deleted.");
        }
    }
}

