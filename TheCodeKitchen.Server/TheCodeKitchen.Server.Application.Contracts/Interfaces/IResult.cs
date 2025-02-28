using TheCodeKitchen.Server.Application.Contracts.Responses;

namespace TheCodeKitchen.Server.Application.Contracts.Interfaces;

public interface IResult
{
    ICollection<Message> Messages { get; set; }

    bool Succeeded { get; set; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}