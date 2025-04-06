using Application.Interfaces;
using Application.Wrappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Features.Authors.Commands;

public record CreateAuthorCommand : IRequest<Result> 
{
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public DateOnly BirthDate { get; init; }
    public string Country { get; init; } = null!;
}

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

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
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