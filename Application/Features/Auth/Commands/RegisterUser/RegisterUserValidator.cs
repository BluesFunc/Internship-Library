using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.Mail).EmailAddress()
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(x => x.Username).MaximumLength(30).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(8).MaximumLength(30);
    }
}