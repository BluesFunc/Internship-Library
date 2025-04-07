using Application.Features.Authors.Commands;
using Application.Features.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstractions;

namespace WebAPI.Controllers;

public class AuthorController(ISender sender) : RestController(sender)
{
    [HttpPost("getById")]
    public async Task<IActionResult> GetById(GetAuthorByIdCommand command)
        => await ExecuteMediatrCommand(command);
    
    [Authorize(Policy = "CreateAuthor")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateAuthorCommand command)
        => await ExecuteMediatrCommand(command);
    
    [Authorize(Policy = "UpdateAuthor")]
    [HttpPost("update")]
    public  async Task<IActionResult> Update(UpdateAuthorCommand command)
        => await ExecuteMediatrCommand(command);
    
    [Authorize(Policy = "DeleteAuthor")]
    [HttpPost("delete")]
    public async Task<IActionResult> Delete(DeleteAuthorByIdCommand command)
        => await ExecuteMediatrCommand(command);

    [AllowAnonymous]
    [HttpGet("get")]
    public async Task<IActionResult> Get(int pageNo, int pageSize)
    {
        var command = new GetPaginatedAuthorsCommand() { PageNo = pageNo, PageSize = pageSize };
        return await ExecuteMediatrCommand(command);
    }
        
}