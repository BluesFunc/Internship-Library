using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.CreateBook;



public class CreateBookHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateBookCommand, Result<Book>>
{

 public async Task<Result<Book>> Handle(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
        if (author == null)
        {
            return Result<Book>.Failed("Author is missing");
        }
        var newBook = new Book()
        {
            AuthorId = request.AuthorId,
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            Isbn = request.Isbn,
            Genre = request.Genre
        };
        var result = await bookRepository.AddAsync(newBook, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);        
        return Result<Book>.Successful(result);
    }
}

