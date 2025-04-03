using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands;

public record UpdateAuthorCommand : IRequest<Result>
{
    public Guid Id { get; init; } 
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public string Birthdate { get; init; } = null!;
    public string Country { get; init; } = null!;
}

public class UpdateAuthorHandler(IAuthorRepository repository) 
    : IRequestHandler<UpdateAuthorCommand, Result>
{

    public async Task<Result> Handle(
        UpdateAuthorCommand request,
        CancellationToken cancellationToken
        )
    {
        var author = await repository.GetByIdAsync(request.Id);
        author = request.Adapt<Author>();
        await repository.UpdateAsync(author);
        return Result.Successful();
    }
}
