using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Abstractions;


[ApiController]
[Route("[controller]")]
public abstract class RestController(ISender sender) : ControllerBase
{
    protected IActionResult ToActionResult(Result result)
    {
        return result.Succeed ? Ok(result) : BadRequest(result.Message);
    }

    protected IActionResult ToActionResult<T> (Result<T> result)
    {
        return result.Succeed ? Ok(result.Content) : BadRequest(result.Message);
    }

    protected async Task<IActionResult> ExecuteMediatrCommand(IRequest<Result> command)
    {
        var result = await sender.Send(command);
        return ToActionResult(result);
    }
    
    
    protected async Task<IActionResult> ExecuteMediatrCommand<T> (IRequest<Result<T>> command)
    {
        var result = await sender.Send(command);
        return ToActionResult(result);
    }
}