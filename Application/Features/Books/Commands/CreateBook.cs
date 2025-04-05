﻿using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Enums;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Commands;

public record CreateBookCommand : IRequest<Result<Book>>
{
    public string Name { get; init; } = null!;
    public string Isbn { get; init; } = null!;
    public BookGenre Genre { get; init; }
    public string Description { get; init; } = null!;
    public string Image { get; init; } = null!;
    public Guid AuthorId { get; init; }
}

public class CreateBookHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IMapper mapper
) : IRequestHandler<CreateBookCommand, Result<Book>>
{

 public async Task<Result<Book>> Handle(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(request.AuthorId);
        if (author == null)
        {
            return Result<Book>.Failed("Author is missing");
        }
        var newBook = new Book()
        {
            Author = author,
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            Isbn = request.Isbn,
            Genre = request.Genre
        };
        var result = await bookRepository.AddAsync(newBook);
        return Result<Book>.Successful(result);
    }
}