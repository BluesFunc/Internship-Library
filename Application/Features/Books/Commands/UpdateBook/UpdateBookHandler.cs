using Domain.Entities;
using Domain.Entities.Abstraction;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Commands.UpdateBook;



public class UpdateBookHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository)
    : IRequestHandler<UpdateBookCommand, Result>
{
   
    public async Task<Result> Handle
        (UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(request.Id, cancellationToken);
        if (book == null)
        {
            return Result.Failed("Book not found", ErrorTypeCode.NotFound);
        }

        var author = await authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
        if (author == null)
        {
            return Result.Failed("Author not found", ErrorTypeCode.NotFound);
        }

        book.Name = request.Name;
        book.Genre = request.Genre;
        book.Description = request.Description;
        book.Image = request.Image;
        book.SetAuthor(author);
        bookRepository.Update(book);
        return Result.Successful();
    }
}

