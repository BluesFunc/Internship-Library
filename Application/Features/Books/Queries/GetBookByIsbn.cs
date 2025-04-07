using Application.DTOs._Book_;
using Application.Interfaces.Repositories;
using Application.QueryParams;
using Application.Wrappers;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Queries;

public record GetBookByIsbnCommand : IRequest<Result<BookDto>>
{
    public string Isbn { get; init; } = null!;
}

public class GetBookByIsbnHandler(
    IBookRepository repository,
    IMapper mapper)
    : IRequestHandler<GetBookByIsbnCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(
        GetBookByIsbnCommand request,
        CancellationToken cancellationToken)
    {
        var filter = new BookQueryParams() { Isbn = request.Isbn }; 
        var book = await repository.GetEntityByFilter(filter);
        if (book == null)
        {
            return Result<BookDto>.Failed("Book not found");
        }
        var bookDto = mapper.Map<BookDto>(book);
        return Result<BookDto>.Successful(bookDto);
    }
}