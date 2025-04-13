using Application.DTOs._Book_;
using Domain.Interfaces.Repositories;
using Domain.Models.QueryParams;
using Domain.Models.Wrappers;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Queries.GetBookByIsbn;



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
            return Result<BookDto>.Failed("Book not found", ErrorTypeCode.NotFound);
        }
        var bookDto = mapper.Map<BookDto>(book);
        return Result<BookDto>.Successful(bookDto);
    }
}