using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ISP.Application.Requests.Identity.Password;

public abstract class ForgotPasswordRequest
{
    public string Email { get; set; } = default!;
}

public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordRequestValidator(IStringLocalizer<ForgotPasswordRequestValidator> T)
    {
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(T["Invalid Email Address."]);
    }
}