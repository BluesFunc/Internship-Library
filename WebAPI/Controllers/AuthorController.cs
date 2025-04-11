using Application.Features.Authors.Commands;
using Application.Features.Authors.Commands.CreateAuthor;
using Application.Features.Authors.Commands.DeleteAuthorById;
using Application.Features.Authors.Commands.UpdateAuthor;
using Application.Features.Authors.Queries;
using Application.Features.Authors.Queries.GetAuthorById;
using Application.Features.Authors.Queries.GetPaginatedAuthors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstractions;

namespace WebAPI.Controllers;

public class AuthorController(ISender sender) : RestController(sender)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new GetAuthorByIdCommand(){Id = id};
        return await ExecuteMediatrCommand(command);
    }
    
    
    [Authorize(Policy = "CreateAuthor")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorCommand command)
        => await ExecuteMediatrCommand(command);
    
    [Authorize(Policy = "UpdateAuthor")]
    [HttpPut]
    public  async Task<IActionResult> Update(UpdateAuthorCommand command)
        => await ExecuteMediatrCommand(command);
    
    [Authorize(Policy = "DeleteAuthor")]
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteAuthorByIdCommand command)
        => await ExecuteMediatrCommand(command);

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get(int pageNo, int pageSize)
    {
        var command = new GetPaginatedAuthorsCommand() { PageNo = pageNo, PageSize = pageSize };
        return await ExecuteMediatrCommand(command);
    }
        
}