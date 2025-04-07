using Application.DTOs._Book_;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using MediatR;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Mapster;
using MapsterMapper;

namespace Application.Features.Books.Commands;

public class UpdateBookCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Isbn { get; init; } = null!;
    public BookGenre Genre { get; init; }
    public string Description { get; init; } = null!;
    public string Image { get; init; } = null!;
    public Guid  AuthorId { get; init; }
}

public class UpdateBookHandler(
    IBookRepository repository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateBookCommand, Result>
{
    private string BookMissingMessage = "Book is missing"; 
    public async Task<Result> Handle
        (UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(request.Id);
        if (book == null)
        {
            return Result.Failed(BookMissingMessage);
        }
            
        book = request.Adapt<Book>();
        await repository.UpdateAsync(book);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
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