namespace TheCodeKitchen.Infrastructure.DataAccess.Abstractions;

public class BaseEntity
{
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}