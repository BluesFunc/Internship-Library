using Application.DTOs._Author_;
using Application.DTOs._Book_;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MapsterMapper;
using MediatR;

namespace Application.Features.Authors.Queries;

public record GetAuthorByIdCommand : IRequest<Result<AuthorDto>>
{
    public Guid Id { get; set; }
}

public class GetAuthorByIdHandler(
    IAuthorRepository repository, IMapper mapper)
    : IRequestHandler<GetAuthorByIdCommand, Result<AuthorDto>>
{
    public async Task<Result<AuthorDto>> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await repository.GetByIdAsync(request.Id);
        var authorDto = mapper.Map<AuthorDto>(author);
        return Result<AuthorDto>.Successful(authorDto);
    }
} 