using Application.DTOs._Account_;
using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IJwtService
{
    public TokenPair GenerateTokenPair(User user);
    
}