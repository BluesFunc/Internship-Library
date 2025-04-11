using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor;



public class CreateAuthorCommandHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork
    ) 
    : IRequestHandler<CreateAuthorCommand, Result>
{
    public async Task<Result> Handle(
        CreateAuthorCommand request,
        CancellationToken cancellationToken
        )
    {

        var author = request.Adapt<Author>();
        var result = await repository.AddAsync(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}
