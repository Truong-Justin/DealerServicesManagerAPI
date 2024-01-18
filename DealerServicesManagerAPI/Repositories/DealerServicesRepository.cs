using Microsoft.Data.SqlClient;
using System.Data;

namespace DealerServicesManagerAPI.Repositories
{
	public class DealerServicesRepository : IDealerServicesRepository
	{
		private readonly string _connectionString;

		public DealerServicesRepository(IConfiguration configuration)
		{
            _connectionString = configuration.GetConnectionString("CONNECTION");
		}

        // Method returns all CustomerServices for each dealership given the dealerId
        public async Task<IEnumerable<CustomerServices>> GetCustomerServicesForDealerAsync(int dealershipId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                List<CustomerServices> customerServices = new List<CustomerServices>();

                using (SqlCommand command = new SqlCommand("GetCustomerServicesByDealer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("DealerId", SqlDbType.Int)).Value = dealershipId;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CustomerServices customerService = new CustomerServices()
                            {
                                DealerName = (string)reader["DealerName"],
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

