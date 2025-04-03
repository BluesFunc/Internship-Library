using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authors.Commands;

public record DeleteAuthorByIdCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}

public class DeleteAuthorByIdHandler(IAuthorRepository repository) : IRequestHandler<DeleteAuthorByIdCommand, Result>
{
    public async Task<Result> Handle(
        DeleteAuthorByIdCommand request, 
        CancellationToken cancellationToken
        )
    {
        await repository.DeleteByIdAsync(request.Id);
        return Result.Successful();
    }
}
