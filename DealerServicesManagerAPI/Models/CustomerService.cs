namespace DealerServicesManagerAPI.Models;

public partial class CustomerService
{
    public int CustomerId { get; set; }

    public int ServiceId { get; set; }

    public DateOnly Date { get; set; }

    public bool? IsComplete { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
