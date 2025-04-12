using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Commands.UpdateBook;



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
        var book = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (book == null)
        {
            return Result.Failed(BookMissingMessage);
        }
            
        book = request.Adapt<Book>();
        repository.Update(book);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}

