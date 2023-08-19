using ISP.Application.Requests.Identity.Role;
using ISP.Application.ViewModels.Role;
using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ISP.Application.Services;

public abstract class IRoleService : RoleManager<Role>
{
    protected IRoleService(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger) : base(store,
        roleValidators, keyNormalizer, errors, logger)
    {
    }

    public abstract Task<bool> InitializeAsync();

    public abstract Task<List<RoleDto>> GetListAsync();

    public abstract Task<int> GetCountAsync();

    public abstract Task<bool> ExistsAsync(string roleName, string? excludeId);

    public abstract Task<RoleDto> GetByIdAsync(string id);

    public abstract Task<IdentityResult> CreateAsync(CreateRoleRequest request);

    public abstract Task<IdentityResult> UpdateAsync(UpdateRoleRequest request);

    public abstract Task<IdentityResult> DeleteAsync(string id);

    public abstract Task AddUserToRoleAsync(string userId, string roleName);

    public abstract Task RemoveUserFromRoleAsync(string userId, string roleName);

    public abstract Task<IList<string>> GetUserRolesAsync(string userId);
}