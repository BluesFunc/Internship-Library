using Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Abstractions;

namespace WebAPI.Controllers;

public class AuthController(ISender sender) : RestController(sender)
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command) 
        => await ExecuteMediatrCommand(command);
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
        => await ExecuteMediatrCommand(command);

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command)
        => await ExecuteMediatrCommand(command);
}