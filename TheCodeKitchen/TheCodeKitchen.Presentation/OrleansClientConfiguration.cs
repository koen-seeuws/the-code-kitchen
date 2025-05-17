using FluentValidation;

namespace TheCodeKitchen.Presentation;

public class OrleansClientConfiguration
{
    public string? ClusterId { get; set; }
    public string? ServiceId { get; set; }
}

public class OrleansClientConfigurationValidator : AbstractValidator<OrleansClientConfiguration>
{
    public OrleansClientConfigurationValidator()
    {
        RuleFor(x => x.ClusterId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();
    }
}