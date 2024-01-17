namespace DealerServicesManagerAPI.Models
{
	public class CustomerServices : CustomerService
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string ServiceName { get; set; }

		public int LaborHours { get; set; }
	}
}

