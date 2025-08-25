using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Business.Validators;

public class SendMessageValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageValidator()
    {
        RuleFor(m => m.Content).NotEmpty();
    }
}