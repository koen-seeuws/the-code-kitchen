using FluentValidation;

namespace TheCodeKitchen.Infrastructure.Orleans;

public class OrleansSiloConfiguration
{
    public string? ClusterId { get; set; }
    public string? ServiceId { get; set; }
}

public class OrleansSiloConfigurationValidator : AbstractValidator<OrleansSiloConfiguration>
{
    public OrleansSiloConfigurationValidator()
    {
        RuleFor(x => x.ClusterId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();
    }
}