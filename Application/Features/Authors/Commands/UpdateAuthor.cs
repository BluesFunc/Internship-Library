using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands;

public record UpdateAuthorCommand : IRequest<Result>
{
    public Guid Id { get; init; } 
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public DateOnly BirthDate { get; init; }
    public string Country { get; init; } = null!;
}

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
        await repository.UpdateAsync(author);
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