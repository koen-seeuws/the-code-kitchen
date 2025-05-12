using FluentValidation;

namespace TheCodeKitchen.Infrastructure.AzureSignalR;

public class AzureSignalRConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;
    public string? ApplicationName { get; set; } = null;
}

public class AzureSignalRConfigurationValidator : AbstractValidator<AzureSignalRConfiguration>
{
    public AzureSignalRConfigurationValidator()
    {
        RuleFor(x => x.ConnectionString).NotEmpty();
    }
}