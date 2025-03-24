namespace TheCodeKitchen.Application.Business.CommandHandlers;

public class JoinGameCommandHandler(
) : IRequestHandler<JoinGameCommand, Result<JoinGameResponse>>
{
    public async Task<Result<JoinGameResponse>> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}