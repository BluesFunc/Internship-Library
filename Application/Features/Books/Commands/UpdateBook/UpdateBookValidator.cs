using FluentValidation;

namespace Application.Features.Books.Commands.UpdateBook;
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Description)
            .MaximumLength(300)
            .NotEmpty();
        RuleFor(x => x.Genre).IsInEnum();


    }
}