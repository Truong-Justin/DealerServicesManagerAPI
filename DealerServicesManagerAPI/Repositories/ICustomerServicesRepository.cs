using System;
using Microsoft.AspNetCore.Mvc;

namespace DealerServicesManagerAPI.Repositories
{
	public interface ICustomerServicesRepository
	{
		Task<IEnumerable<CustomerServices>> GetCustomerServicesAsync();
	}
}

