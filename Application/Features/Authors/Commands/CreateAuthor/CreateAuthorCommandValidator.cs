using FluentValidation;

namespace Application.Features.Authors.Commands.CreateAuthor;


public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Surname)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today));
        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(30);


    }
}