namespace TheCodeKitchen.Application.Contracts.Response.Cook;

public record GetTimerResponse(int Number, TimeSpan Time, string Note);