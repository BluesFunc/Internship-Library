using Application.Wrappers;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Authors.Commands.DeleteAuthorById;


public class DeleteAuthorByIdHandler(IAuthorRepository repository) : IRequestHandler<DeleteAuthorByIdCommand, Result>
{
    public async Task<Result> Handle(
        DeleteAuthorByIdCommand request, 
        CancellationToken cancellationToken
        )
    {
        var author = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (author == null)
        {
            return Result.Failed("Author not found", ErrorTypeCode.NotFound);
        }
        repository.Delete(author);
        return Result.Successful();
    }
}
