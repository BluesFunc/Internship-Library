using Application.DTOs._Author_;
using Application.Wrappers;
using Domain.Interfaces.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Features.Authors.Queries.GetAuthorById;



public class GetAuthorByIdHandler(
    IAuthorRepository repository, IMapper mapper)
    : IRequestHandler<GetAuthorByIdCommand, Result<AuthorDto>>
{
    public async Task<Result<AuthorDto>> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await repository.GetByIdAsync(request.Id, cancellationToken);
        var authorDto = mapper.Map<AuthorDto>(author);
        return Result<AuthorDto>.Successful(authorDto);
    }
} 