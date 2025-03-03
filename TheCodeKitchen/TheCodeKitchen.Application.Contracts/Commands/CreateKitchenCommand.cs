using MediatR;
using TheCodeKitchen.Core.Shared.Monads;

namespace TheCodeKitchen.Application.Contracts.Commands;

public class CreateKitchenCommand : IRequest<Result<CreateKitchenRequestResult>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public long GameId { get; set; }
}

public class CreateKitchenRequestResult
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long GameId { get; set; }
}