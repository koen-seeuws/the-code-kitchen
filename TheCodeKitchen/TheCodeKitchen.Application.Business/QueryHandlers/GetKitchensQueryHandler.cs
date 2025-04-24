using TheCodeKitchen.Application.Contracts.Queries;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.QueryHandlers;

public class GetKitchensQueryValidator : AbstractValidator<GetKitchensQuery>
{
    public GetKitchensQueryValidator()
    {
        RuleFor(x => x.GameId).NotEmpty();
    }
}

public class GetKitchensQueryHandler(
    IKitchenRepository kitchenRepository,
    IMapper mapper
) : IRequestHandler<GetKitchensQuery, Result<IEnumerable<GetKitchensResponse>>>
{
    public async Task<Result<IEnumerable<GetKitchensResponse>>> Handle(GetKitchensQuery request,
        CancellationToken cancellationToken)
    {
        var kitchens =  await kitchenRepository.GetByGameId(request.GameId, cancellationToken);
        var kitchenResponses = mapper.Map<List<GetKitchensResponse>>(kitchens);
        return kitchenResponses;
    }
}