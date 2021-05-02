using MediatR_API_Example.Domain;
using Microsoft.EntityFrameworkCore;

namespace MediatR_API_Example.Infrastructure
{
    public interface IExampleDataContext
    {
        DbSet<User> User { get; set; }
        DbSet<Sex> Sex { get; set; }
    }
}
