using Application.DTOs._Book_;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Queries;

public record GetBookByIdCommand : IRequest<Result<BookDto>>
{
    public Guid Id { get; init; }
}

public class GetBookByIdHandler(
    IBookRepository repository,
    IMapper mapper)
    : IRequestHandler<GetBookByIdCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(request.Id);
        var bookDto = mapper.Map<BookDto>(book);
        return Result<BookDto>.Successful(bookDto);
    }
}