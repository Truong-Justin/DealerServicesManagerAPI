using Microsoft.Data.SqlClient;
using System.Data;

namespace DealerServicesManagerAPI.Repositories
{
	public class CustomerServicesRepository : ICustomerServicesRepository
	{
		private readonly string _connectionString;

		public CustomerServicesRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("CONNECTION");
		}

		// Method returns all CustomerServices by executing a stored procedure
		public async Task<IEnumerable<CustomerServices>> GetCustomerServicesAsync()
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				List<CustomerServices> customerServices = new List<CustomerServices>();

				using (SqlCommand command = new SqlCommand("EXEC GetCustomerServices", connection))
				{
					command.CommandType = CommandType.Text;

					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							CustomerServices customerService = new CustomerServices()
							{
								ServiceId = Convert.ToInt32(reader["ServiceId"]),
								CustomerId = Convert.ToInt32(reader["CustomerId"]),
								FirstName = (string)reader["FirstName"],
								LastName = (string)reader["LastName"],
								ServiceName = (string)reader["ServiceName"],
								LaborHours = Convert.ToInt32(reader["Labor_Hours"]),
								Date = DateOnly.FromDateTime((DateTime)reader["Date"]),
								IsComplete = (bool)reader["IsComplete"]
							};

							customerServices.Add(customerService);
						}
					}
				}

                return customerServices;
            }
		}
	}
}

