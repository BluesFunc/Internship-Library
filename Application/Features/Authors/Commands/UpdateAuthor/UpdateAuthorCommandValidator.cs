using FluentValidation;

namespace Application.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
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