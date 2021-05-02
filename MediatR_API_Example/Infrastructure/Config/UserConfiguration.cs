using MediatR_API_Example.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatR_API_Example.Infrastructure.Config
{
    public class UserConfiguration
         : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName)
                .HasMaxLength(25)
                .IsRequired();
            builder.Property(x => x.LastName)
                .HasMaxLength(25)
                .IsRequired();
            builder.Property(x => x.SexId)
                .IsRequired();
        }
    }
}