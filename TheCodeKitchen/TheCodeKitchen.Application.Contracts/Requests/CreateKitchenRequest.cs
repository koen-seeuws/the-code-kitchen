namespace TheCodeKitchen.Application.Contracts.Requests;

public record CreateKitchenRequest
{
    public string Name { get; set; }
    public string Code { get; set; }
    public long GameId { get; set; }
}