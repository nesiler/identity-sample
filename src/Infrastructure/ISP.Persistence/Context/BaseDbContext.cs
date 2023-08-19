using System.Data;
using ISP.Domain.Entities.Common;
using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISP.Persistence.Context;

public abstract class BaseDbContext : IdentityDbContext<User, Role, string,
    UserClaim, UserRole, IdentityUserLogin<string>, RoleClaim,
    IdentityUserToken<string>>
{
    private readonly ICurrentUser _currentUser;

    protected BaseDbContext(DbContextOptions options, ICurrentUser currentUser)
        : base(options)
    {
        _currentUser = currentUser;
    }

    // Used by Dapper
    public IDbConnection Connection => Database.GetDbConnection();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        // QueryFilters need to be applied before base.OnModelCreating
        builder.AppendGlobalQueryFilter<ISoftDelete>(s => s.IsDeleted != true);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        HandleAuditingBeforeSaveChanges(_currentUser.GetUserId());

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void HandleAuditingBeforeSaveChanges(string userId)
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedBy = userId;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Deleted:
                    if (entry.Entity is ISoftDelete softDelete)
                    {
                        softDelete.IsDeleted = true;
                        softDelete.DeletedBy = userId;
                        softDelete.DeletedAt = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                    }

                    break;
            }

        ChangeTracker.DetectChanges();
    }
}