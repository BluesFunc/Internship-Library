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

public class CreateAuthorCommandHandler(IAuthorRepository repository) 
    : IRequestHandler<CreateAuthorCommand, Result>
{
    public async Task<Result> Handle(
        CreateAuthorCommand request,
        CancellationToken cancellationToken
        )
    {

        var author = request.Adapt<Author>();
        var result = await repository.AddAsync(author);
        return Result.Successful();
    }
}

class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);


    }
}