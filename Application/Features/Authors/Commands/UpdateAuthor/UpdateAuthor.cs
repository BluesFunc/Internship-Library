using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands.UpdateAuthor;



public class UpdateAuthorHandler(IAuthorRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateAuthorCommand, Result>
{

    public async Task<Result> Handle(
        UpdateAuthorCommand request,
        CancellationToken cancellationToken
        )
    {
        var author = await repository.GetByIdAsync(request.Id);
        author = request.Adapt<Author>();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}

public class UpdateAuthorValidation : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Surname)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today));
        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(30);
    }
}