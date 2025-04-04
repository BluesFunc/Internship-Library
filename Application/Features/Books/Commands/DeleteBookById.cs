using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands;

public record DeleteBookByIdCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}

public class DeleteBookByIdHandler(IBookRepository repository) : IRequestHandler<DeleteBookByIdCommand, Result>
{
    public async Task<Result> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteByIdAsync(request.Id);
        return Result.Successful();
    }
}