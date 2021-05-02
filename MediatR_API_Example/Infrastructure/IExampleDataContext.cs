using MediatR_API_Example.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_API_Example.Infrastructure
{
    public interface IExampleDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<User> User { get; set; }
        DbSet<Sex> Sex { get; set; }
    }
}
