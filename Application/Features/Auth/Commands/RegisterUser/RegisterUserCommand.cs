﻿using Application.DTOs._Account_;
using Application.Interfaces.Requests;
using Domain.Enums;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.RegisterUser;

public record RegisterUserCommand : IRequest<Result<TokenPair>>, ITransactionRequest
{
    public string Username { get; init; }
    public string Mail { get; init; }
    public string Password { get; init; }
    public UserRole Role { get; init; }
}
