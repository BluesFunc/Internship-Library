using Application.DTOs._Book_;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Queries.GetBook;



public class GetBookByIdHandler(
    IBookRepository repository,
    IMapper mapper)
    : IRequestHandler<GetBookByIdCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(request.Id, cancellationToken);
        var bookDto = mapper.Map<BookDto>(book);
        return Result<BookDto>.Successful(bookDto);
    }
}