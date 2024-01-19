using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace DealerServicesManagerAPI.Controllers
{
    [Route("api/CustomerServices")]
    public class CustomerServicesController : ControllerBase
    {
        private readonly ICustomerServicesRepository _customerServicesRepository;

        public CustomerServicesController(ICustomerServicesRepository customerServicesRepository)
        {
            _customerServicesRepository = customerServicesRepository;
        }

        [HttpGet]
        [Route("GetAllCustomerServices")]
        public async Task<ActionResult> GetAllCustomerServices()
        {
            return Ok(await _customerServicesRepository.GetCustomerServicesAsync());
        }

        [HttpGet]
        [Route("GetCustomerServiceById")]
        public async Task<ActionResult> GetCustomerById(int customerId, int serviceId)
        {
            return Ok(await _customerServicesRepository.GetCustomerServiceByIdsAsync(customerId, serviceId));
        }
    }
}

