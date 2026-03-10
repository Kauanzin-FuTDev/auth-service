using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructer.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.CPF)
            .HasMaxLength(14);

        builder.Property(x => x.Address)
            .HasMaxLength(256);
        
        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(x => x.Password)
            .HasMaxLength(256)
            .IsRequired();
    }
}