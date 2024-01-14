namespace DealerServicesManagerAPI.Models;

public partial class Dealership
{
    public int DealerId { get; set; }

    public string DealerName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
