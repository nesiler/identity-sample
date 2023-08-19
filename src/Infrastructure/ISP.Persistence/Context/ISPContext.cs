using ISP.Domain.Entities.Common;
using ISP.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ISP.Persistence.Context;

public class ISPContext : BaseDbContext
{
    public ISPContext(DbContextOptions options, ICurrentUser currentUser) : base(options, currentUser)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema(SchemaNames.Identity);
    }
}