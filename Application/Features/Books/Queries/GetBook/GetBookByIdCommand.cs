using Application.DTOs._Book_;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Queries.GetBook;

public record GetBookByIdCommand : IRequest<Result<BookDto>>
{
    public Guid Id { get; init; }
}