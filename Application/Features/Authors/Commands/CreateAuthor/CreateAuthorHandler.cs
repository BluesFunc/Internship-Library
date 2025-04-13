using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor;



public class CreateAuthorCommandHandler(
    IAuthorRepository repository
    ) 
    : IRequestHandler<CreateAuthorCommand, Result>
{
    public async Task<Result> Handle(
        CreateAuthorCommand request,
        CancellationToken cancellationToken
        )
    {

        var author = new Author(request.Name, request.Surname, request.BirthDate, request.Country);
        var result = await repository.AddAsync(author, cancellationToken);
         return Result.Successful();
    }
}
