﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DealerServicesManagerAPI.Controllers
{
    [Route("api/Dealership")]
    [ApiController]
    public class DealershipController : ControllerBase
    {
        private readonly DealerServicesDbContext _context;
        private readonly IDealerServicesRepository _dealerServicesRepository;

        // DealerServicesDbContext is dependency injected into
        // class controller so the entire DealershipController
        // class can use DealerShipServicesDbContext methods.
        public DealershipController(DealerServicesDbContext context, IDealerServicesRepository dealerServicesRepository)
        {
            _context = context;
            _dealerServicesRepository = dealerServicesRepository;
        }

        // 
        [HttpGet]
        [Route("GetAllDealerships")]
        public async Task<ActionResult> GetAllDealerships()
        {
            return Ok(await _context.Dealerships.ToListAsync());
        }

        [HttpGet]
        [Route("GetEmployeesForDealership")]
        public async Task<ActionResult> GetEmployeesForDealership(int dealershipId)
        {
            var query = from a in _context.Dealerships
                        join b in _context.Employees on a.DealerId equals b.DealerId
                        where a.DealerId == dealershipId
                        select new
                        {
                            EmployeeId = b.EmployeeId,
                            FirstName = b.FirstName,
                            LastName = b.LastName
                        };

            return Ok(await query.ToListAsync());
        }

        [HttpGet]
        [Route("GetCustomersForDealership")]
        public async Task<ActionResult> GetCustomersForDealership(int dealershipId)
        {
            var query = from a in _context.Dealerships
                        join b in _context.Customers on a.DealerId equals b.DealerId
                        where a.DealerId == dealershipId
                        select new
                        {
                            CustomerId = b.CustomerId,
                            FirstName = b.FirstName,
                            LastName = b.LastName
                        };

            return Ok(await query.ToListAsync());
        }

        [HttpGet]
        [Route("GetCustomerServicesForDealer")]
        public async Task<ActionResult> GetCustomerServicesForDealer(int dealershipId)
        {
            return Ok(await _dealerServicesRepository.GetCustomerServicesForDealerAsync(dealershipId));
        }

        [HttpGet]
        [Route("GetDealershipById")]
        public async Task<ActionResult> GetDealershipById(int dealershipId)
        {
            Dealership dealership = await _context.Dealerships.FindAsync(dealershipId);

            if (dealership == null)
            {
                return NotFound("A dealership with the given Id doesn't exist.");
            }

            return Ok(dealership);
        }

        [HttpPost]
        [Route("AddDealership")]
        public async Task<ActionResult> AddDealership(string name, string address, string zip, string state, string city, string phone, string email)
        {
            await _context.Dealerships.AddAsync(new Dealership()
            {
                DealerName = name,
                Address = address,
                Zip = zip,
                State = state,
                City = city,
                Phone = phone,
                Email = email
            });

            await _context.SaveChangesAsync();

            return Ok("A new dealership has been added.");
        }

        [HttpPut]
        [Route("UpdateDealership")]
        public async Task<ActionResult> UpdateDealership(int dealerId, string name, string address, string zip, string state, string city, string phone, string email)
        {
            Dealership dealer = await _context.Dealerships.Where(d => d.DealerId == dealerId).FirstOrDefaultAsync<Dealership>();

            if (dealer != null)
            {
                dealer.DealerName = name;
                dealer.Address = address;
                dealer.Zip = zip;
                dealer.State = state;
                dealer.City = city;
                dealer.Phone = phone;
                dealer.Email = email;

                await _context.SaveChangesAsync();
            }

            else
            {
                return NotFound("A dealership with the given Id doesn't exist.");
            }

            return Ok("The dealership has been updated");
        }

        [HttpDelete]
        [Route("DeleteDealership")]
        public async Task<ActionResult> DeleteDealership(int dealershipId)
        {
            Dealership dealership = await _context.Dealerships.FindAsync(dealershipId);
            if (dealership == null)
            {
                return NotFound("A dealership with the given Id doesn't exist.");
            }

            _context.Dealerships.Remove(dealership);
            await _context.SaveChangesAsync();

            return Ok("The dealership has been deleted.");
        }
    }
}

