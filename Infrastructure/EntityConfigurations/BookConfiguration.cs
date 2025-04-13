using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(nameof(Book));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property((x => x.Isbn))
            .HasMaxLength(13)
            .IsRequired();

        builder.HasOne<User>(book => book.BookedBy)
            .WithMany(user => user.ReservedBooks)
            .HasForeignKey(book => book.BookedById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasIndex(x => x.Isbn).IsUnique();
    }
}