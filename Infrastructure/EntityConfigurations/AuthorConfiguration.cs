using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable(nameof(Author));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Surname)
            .HasMaxLength(30)
            .IsRequired();
        builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();
        builder.Property(x => x.Country)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasMany<Book>(x => x.Books)
            .WithOne(book => book.Author)
            .HasForeignKey(book => book.AuthorId);
    }
}