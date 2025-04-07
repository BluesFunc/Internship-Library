using Domain.Enums;
using Mapster;

namespace Application.DTOs._Account_;

public class UserDto 
{
    public Guid Id { get; set; }
    public UserRole Role { get; set; }
}