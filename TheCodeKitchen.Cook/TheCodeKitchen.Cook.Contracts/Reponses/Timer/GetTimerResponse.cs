namespace TheCodeKitchen.Cook.Contracts.Reponses.Timer;

public record GetTimerResponse(int Number, TimeSpan Time, string Note);