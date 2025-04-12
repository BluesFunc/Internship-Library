using Application.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Authors.Commands.DeleteAuthorById;


public class DeleteAuthorByIdHandler(IAuthorRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteAuthorByIdCommand, Result>
{
    public async Task<Result> Handle(
        DeleteAuthorByIdCommand request, 
        CancellationToken cancellationToken
        )
    {
        var author = await repository.GetByIdAsync(request.Id, cancellationToken);
        repository.Delete(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}
