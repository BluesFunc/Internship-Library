using Application.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.DeleteBook;

public class DeleteBookHandler(IBookRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteBookCommand, Result>
{
    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(request.Id, cancellationToken);
        repository.Delete(book);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}