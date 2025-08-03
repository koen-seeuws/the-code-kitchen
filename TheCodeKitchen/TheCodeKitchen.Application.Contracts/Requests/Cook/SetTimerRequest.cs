namespace TheCodeKitchen.Application.Contracts.Requests.Cook;

public record SetTimerRequest(TimeSpan Time, string Note);