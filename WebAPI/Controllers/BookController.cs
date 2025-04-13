using System.Security.Claims;
using Application.Features.Books.Commands;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.DeleteBook;
using Application.Features.Books.Commands.ReserveBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries;
using Application.Features.Books.Queries.GetBook;
using Application.Features.Books.Queries.GetBookByAuthor;
using Application.Features.Books.Queries.GetBookByIsbn;
using Application.Features.Books.Queries.GetPaginatedBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstractions;

namespace WebAPI.Controllers;

public class BookController(ISender sender) : RestController(sender)
{
    [Authorize(Policy = "CreateBook")]
    [HttpPost()]
    public async Task<IActionResult> Create(CreateBookCommand command)
        => await ExecuteMediatrCommand(command);

    [Authorize(Policy = "UpdateBook")]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateBookCommand command)
        => await ExecuteMediatrCommand(command);

    [Authorize(Policy = "DeleteBook")]
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteBookCommand command)
        => await ExecuteMediatrCommand(command);


    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get(int pageNo = 1, int pageSize = 5)
    {
        var command = new GetPaginatedBooksCommand() { PageNo = pageNo, PageSize = pageSize };
        return await ExecuteMediatrCommand(command);
    }

    [Authorize(Policy = "ReserveBook")]
    [HttpPost("Reserve")]
    public async Task<IActionResult> ReserveBook(ReserveBookCommand command)
        => await ExecuteMediatrCommand(command);

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var command = new GetBookByIdCommand() { Id = id };
        return await ExecuteMediatrCommand(command);
    }


    [HttpGet("getByIsbn")]
    public async Task<IActionResult> GetByIsbn(string isbn)
    {
        var command = new GetBookByIsbnCommand() { Isbn = isbn };
        return await ExecuteMediatrCommand(command);
    }

    [HttpGet("getByAuthor")]
    public async Task<IActionResult> GetByAuthor(Guid authorId, int pageNo = 1, int pageSize = 5)
    {
        var command = new GetBooksByAuthorCommand()
        {
            AuthorId = authorId, PageSize = pageSize, PageNo = pageNo
        };
        return await ExecuteMediatrCommand(command);
    }
}