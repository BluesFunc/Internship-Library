using FluentValidation;

namespace Application.Features.Auth.Commands.LoginUser;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        
        RuleFor(x => x.Mail).
            EmailAddress()
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(x => x.Mail)
            .EmailAddress()
            .MaximumLength(50)
            .NotEmpty();
        
    }
}