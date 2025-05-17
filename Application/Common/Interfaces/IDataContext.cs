using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IDataContext
{
    public DbSet<TokenUsage> TokenUsages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
