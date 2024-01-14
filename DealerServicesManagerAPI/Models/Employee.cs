using System.Text.Json.Serialization;

namespace DealerServicesManagerAPI.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int? DealerId { get; set; }

    [JsonIgnore]
    public virtual Dealership? Dealer { get; set; }
}
