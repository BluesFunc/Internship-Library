using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.DeleteBook;

public class DeleteBookHandler(IBookRepository repository)
    : IRequestHandler<DeleteBookCommand, Result>
{
    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (book == null)
        {
            return Result.Failed("Book not found", ErrorTypeCode.NotFound);
        }
        repository.Delete(book);
         return Result.Successful();
    }
}