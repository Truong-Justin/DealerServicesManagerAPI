using System;
namespace DealerServicesManagerAPI.Repositories
{
	public interface IDealerServicesRepository
	{
		Task<IEnumerable<CustomerServices>> GetCustomerServicesForDealerAsync(int dealershipId);
	}
}

