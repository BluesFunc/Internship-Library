using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authors.Commands;

public record DeleteAuthorByIdCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}

public class DeleteAuthorByIdHandler(IAuthorRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteAuthorByIdCommand, Result>
{
    public async Task<Result> Handle(
        DeleteAuthorByIdCommand request, 
        CancellationToken cancellationToken
        )
    {
        await repository.DeleteByIdAsync(request.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}
