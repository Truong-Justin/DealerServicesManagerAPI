namespace DealerServicesManagerAPI.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public int LaborHours { get; set; }

    public virtual ICollection<CustomerService> CustomerServices { get; set; } = new List<CustomerService>();
}
