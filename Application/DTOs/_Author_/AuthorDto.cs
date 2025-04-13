using Application.DTOs._Book_;
using Domain.Entities;
using Mapster;

namespace Application.DTOs._Author_;

public class AuthorDto : IMapFrom<Author>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string BirthDate { get; set; } = null!;
    public string Country { get; set; } = null!;
    
    
}