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
    public async Task<IActionResult> Update(UpdateBookCommand command)
        => await ExecuteMediatrCommand(command);

    [Authorize(Policy = "DeleteBook")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBookByIdCommand(){Id = id};
        return await ExecuteMediatrCommand(command);
    }


    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IActionResult> Get(int pageNo, int pageSize)
    {
        var command = new GetPaginatedBooksCommand() { PageNo = pageNo, PageSize = pageSize };
        return await ExecuteMediatrCommand(command);
    }

    [Authorize(Policy = "ReserveBook")]
    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveBook(ReserveBookCommand command)
        => await ExecuteMediatrCommand(command);
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(GetBookByIdCommand command)
        => await ExecuteMediatrCommand(command);
    
    
    [HttpPost("getByIsbn")]
    public async Task<IActionResult> GetByIsbn(GetBookByIsbnCommand command)
        => await ExecuteMediatrCommand(command);
    
    [HttpPost("getByAuthor")]
    public async Task<IActionResult> GetByAuthor(GetBooksByAuthorCommand command)
        => await ExecuteMediatrCommand(command);

}