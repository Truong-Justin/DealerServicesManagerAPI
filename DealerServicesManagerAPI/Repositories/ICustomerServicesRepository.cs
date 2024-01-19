namespace DealerServicesManagerAPI.Repositories
{
	public interface ICustomerServicesRepository
	{
		Task<IEnumerable<CustomerServices>> GetCustomerServicesAsync();
		Task<CustomerServices> GetCustomerServiceByIdsAsync(int customerId, int serviceId);
	}
}

