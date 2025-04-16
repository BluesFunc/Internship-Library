using Application.DTOs._Book_;
using Application.Interfaces.Requests;
using Application.Wrappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Commands.CreateBook;

public class CreateBookHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IMapper mapper
) : IRequestHandler<CreateBookCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
        if (await bookRepository.IsExistAsync(x => x.Isbn == request.Isbn))
        {
            return Result<BookDto>.Failed("Given ISBN already in use", ErrorTypeCode.EntityConflict);
        }

        var author = await authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
        if (author == null)
        {
            return Result<BookDto>.Failed("Author is missing", ErrorTypeCode.NotFound);
        }

        var newBook = new Book(request.Name,
            request.Isbn, request.Genre, request.Description, request.Image, author);

        await bookRepository.AddAsync(newBook, cancellationToken);
        var result = mapper.Map<BookDto>(newBook);        
        return Result<BookDto>.Successful(result);
    }
}