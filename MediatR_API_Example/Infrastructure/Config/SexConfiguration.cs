using MediatR_API_Example.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatR_API_Example.Infrastructure.Config
{
    public class SexConfiguration
        : IEntityTypeConfiguration<Sex>
    {
        public void Configure(EntityTypeBuilder<Sex> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code)
                .HasMaxLength(10);
            builder.Property(x => x.Name)
                .HasMaxLength(25);
            builder.Property(x => x.Description)
                .HasMaxLength(200);

            builder.HasData(
                new Sex { Id = 1, Code = "M", Name = "Male" },
                new Sex { Id = 2, Code = "F", Name = "Female" }
                );
        }
    }
}
