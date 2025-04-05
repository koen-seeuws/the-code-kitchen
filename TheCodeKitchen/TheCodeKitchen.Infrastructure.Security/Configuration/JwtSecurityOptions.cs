using FluentValidation;

namespace TheCodeKitchen.Infrastructure.Security.Configuration;

public class JwtSecurityOptions
{
    public string Secret { get; set; } = string.Empty;
    public int ValidForHours { get; set; }
}

public class JwtSecurityOptionsValidator : AbstractValidator<JwtSecurityOptions>
{
    public JwtSecurityOptionsValidator()
    {
        RuleFor(x => x.Secret).NotEmpty().MinimumLength(32);
    }
}