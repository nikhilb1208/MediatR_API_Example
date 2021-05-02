using MediatR_API_Example.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MediatR_API_Example.Infrastructure
{
    public class ExampleDataContext : DbContext, IExampleDataContext
    {
        public ExampleDataContext(DbContextOptions<ExampleDataContext> options) : base(options)
        { }
        public DbSet<User> User { get; set; }
        public DbSet<Sex> Sex { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExampleDataContext).Assembly);
        }
    }
}
