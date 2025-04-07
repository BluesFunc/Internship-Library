using System.Security.Claims;
using Application.Features.Books.Commands;
using Application.Features.Books.Queries;
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
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(UpdateBookCommand command, Guid id)
        => await ExecuteMediatrCommand(command);

    [Authorize(Policy = "DeleteBook")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBookByIdCommand() { Id = id };
        return await ExecuteMediatrCommand(command);
    }


    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get(int pageNo, int pageSize)
    {
        var command = new GetPaginatedBooksCommand() { PageNo = pageNo, PageSize = pageSize };
        return await ExecuteMediatrCommand(command);
    }

    [Authorize(Policy = "ReserveBook")]
    [HttpPost("Reserve/{bookId:guid}")]
    public async Task<IActionResult> ReserveBook(Guid bookId)
    {
        var userId = Guid.Parse(HttpContext.User
            .Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value);
        if (userId == null)
            return BadRequest("User id is missing");
        var command = new ReserveBookCommand() { UserId = userId, BookId = bookId };
        return await ExecuteMediatrCommand(command);
    }

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
    public async Task<IActionResult> GetByAuthor(Guid authorId, int pageNo, int pageSize)
    {
        var command = new GetBooksByAuthorCommand()
        {
            AuthorId = authorId, PageSize = pageSize, PageNo = pageNo
        };
        return await ExecuteMediatrCommand(command);
    }
}