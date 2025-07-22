using FluentValidation;

namespace TheCodeKitchen.Presentation;

public class OrleansClientConfiguration
{
    public string? ClusterId { get; set; }
    public string? ServiceId { get; set; }
    public StreamingEventHubConfiguration? Streaming { get; set; }
}

public class OrleansClientConfigurationValidator : AbstractValidator<OrleansClientConfiguration>
{
    public OrleansClientConfigurationValidator()
    {
        RuleFor(x => x.ClusterId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();
        RuleFor(x => x.Streaming).NotEmpty().SetValidator(new StreamingEventHubConfigurationValidator()!);
    }
}

public class StreamingEventHubConfiguration
{
    public string? EventHub { get; set; }
    public string? ConsumerGroup { get; set; }
}

public class StreamingEventHubConfigurationValidator : AbstractValidator<StreamingEventHubConfiguration>
{
    public StreamingEventHubConfigurationValidator()
    {
        RuleFor(x => x.EventHub).NotEmpty();
        RuleFor(x => x.ConsumerGroup).NotEmpty();
    }
}