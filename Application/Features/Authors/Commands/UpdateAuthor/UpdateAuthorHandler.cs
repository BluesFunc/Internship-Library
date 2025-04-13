using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands.UpdateAuthor;



public class UpdateAuthorHandler(IAuthorRepository repository) 
    : IRequestHandler<UpdateAuthorCommand, Result>
{

    public async Task<Result> Handle(
        UpdateAuthorCommand request,
        CancellationToken cancellationToken
        )
    {
        var author = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (author == null)
        {
            return Result.Failed("Author not found", ErrorTypeCode.NotFound);
        }
        author.Name = request.Name;
        author.Surname = request.Surname;
        author.BirthDate = request.BirthDate;
        author.Country = request.Country;
        
        return Result.Successful();
    }
}

