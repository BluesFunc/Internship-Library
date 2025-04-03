using Domain.Entities;
using Domain.Enums;
using Mapster;

namespace Application.DTOs._Book_;

public class BookDto : IMapFrom<Book>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public BookGenre Genre { get; set; }
    public string Description { get; set; } = null!;
    public bool IsBooked { get; set; }
    public string Image { get; set; } = null!;
    public Author Author { get; set; } = null!;
    
    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookDto>()
            .Map(dest => dest.IsBooked, src => src.BookedBy != null)
            .RequireDestinationMemberSource(true);
    }
}