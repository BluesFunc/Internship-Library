using Application.Features.Authors.Commands;
using Application.Features.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(ISender sender) : ControllerBase
{
    [HttpPost("create")]
    public  async Task<IActionResult> Create(CreateAuthorCommand command)
    {
       var result = await sender.Send(command);
       return Ok(result);
       
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(int pageNo, int pageSize)
    {
        var command = new GetPaginatedAuthorsCommand() { PageNo = pageNo, PageSize = pageSize };
        var result = await sender.Send(command);
        return Ok(result);
    }
        
}