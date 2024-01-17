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
    }
}

