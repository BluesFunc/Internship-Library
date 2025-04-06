using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.Property(x => x.Username)
            .HasMaxLength(30)
            .IsRequired();
        builder.Property(x => x.Mail)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(x => x.Password)
            .HasMaxLength(300)
            .IsRequired();
        builder.HasIndex(x => x.Mail)
            .IsUnique();
    }
}