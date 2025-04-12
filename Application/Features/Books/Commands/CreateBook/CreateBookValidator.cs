using FluentValidation;

namespace Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Isbn)
            .NotEmpty()
            .MaximumLength(13);
        RuleFor(x => x.Description)
            .MaximumLength(300)
            .NotEmpty();
        RuleFor(x => x.Genre).IsInEnum();


    }
}