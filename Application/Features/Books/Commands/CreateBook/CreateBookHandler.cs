using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.CreateBook;

public class CreateBookHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository
) : IRequestHandler<CreateBookCommand, Result<Book>>
{
    public async Task<Result<Book>> Handle(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
        if (await bookRepository.IsExistAsync(x => x.Isbn == request.Isbn))
        {
            return Result<Book>.Failed("Given ISBN already in use");
        }

        var author = await authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
        if (author == null)
        {
            return Result<Book>.Failed("Author is missing");
        }

        var newBook = new Book(request.Name,
            request.Isbn, request.Genre, request.Description, request.Image, author);

        var result = await bookRepository.AddAsync(newBook, cancellationToken);
        return Result<Book>.Successful(result);
    }
}