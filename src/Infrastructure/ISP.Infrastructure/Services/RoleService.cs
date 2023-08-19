using AutoMapper;
using ISP.Application.Exceptions;
using ISP.Application.Requests.Identity.Role;
using ISP.Application.Services;
using ISP.Application.Validators.Identity;
using ISP.Application.ViewModels.Role;
using ISP.Domain.Authorization;
using ISP.Domain.Entities.Common;
using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ISP.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly ErrorDescriber _errorDescriber;
    private readonly IMapper _mapper;
    private readonly IUserService _userManager;

    public RoleService(
        ErrorDescriber errorDescriber,
        ICurrentUser currentUser,
        IMapper mapper,
        IUserService userManager,
        IRoleStore<Role> store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<Role>> logger) : base(store,
        roleValidators, keyNormalizer, errors, logger)
    {
        _errorDescriber = errorDescriber;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async override Task<bool> InitializeAsync()
    {
        foreach (var roleName in ISPRoles.DefaultRoles)
            if (await Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not Role role)
                await CreateAsync(new Role(roleName, $"{roleName} Role"));

        //create root user, check if exists
        if (await _userManager.ExistsWithUserNameAsync("root"))
            return true;

        var rootUser = new User
        {
            UserName = "root",
            Email = "root@root.com",
            FirstName = "root",
            LastName = "root",
            PhoneNumber = "1234567890"
        };

        rootUser.PasswordHash = _userManager.PasswordHasher.HashPassword(rootUser, "0000");

        var user = await _userManager.CreateAsync(rootUser);

        if (user.Succeeded && !await _userManager.IsInRoleAsync(rootUser, ISPRoles.Admin))
            await _userManager.AddToRoleAsync(rootUser, ISPRoles.Admin);

        return user.Succeeded;
    }


    public async override Task<List<RoleDto>> GetListAsync() =>
        _mapper.Map<List<RoleDto>>(await Roles.ToListAsync());

    public async override Task<int> GetCountAsync() =>
        await Roles.CountAsync();

    public async override Task<bool> ExistsAsync(string roleName, string? excludeId) =>
        await FindByNameAsync(roleName)
            is Role existingRole
        && existingRole.Id != excludeId;

    public async override Task<RoleDto> GetByIdAsync(string id) =>
        await Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
            ? _mapper.Map<RoleDto>(role)
            : throw new NotFoundException("Role Not Found");

    public async override Task<IdentityResult> CreateAsync(CreateRoleRequest request) =>
        (await base.CreateAsync(new Role(request.Name, request.Description))).Succeeded
            ? IdentityResult.Success
            : IdentityResult.Failed(_errorDescriber.DefaultError());


    public async override Task<IdentityResult> UpdateAsync(UpdateRoleRequest request) =>
        await base.UpdateAsync(new Role(request.Id, request.Name, request.Description));


    public async override Task<IdentityResult> DeleteAsync(string id)
    {
        var role = await FindByIdAsync(id);

        if (role is null) return IdentityResult.Failed(_errorDescriber.RoleNotFound());

        if (ISPRoles.IsDefault(role.Name!))
            return IdentityResult.Failed(_errorDescriber.RoleCanNotBeDeleted());

        if ((await _userManager.GetUsersInRoleAsync(role.Name!)).Count > 0)
            return IdentityResult.Failed(_errorDescriber.RoleHasUsers());

        return await base.DeleteAsync(role);
    }

    public async override Task AddUserToRoleAsync(string userId, string roleName)
        => await _userManager.AddToRoleAsync(
            await _userManager.FindByIdAsync(userId) ?? throw new ArgumentNullException(nameof(userId)), roleName);

    public async override Task RemoveUserFromRoleAsync(string userId, string roleName)
        => await _userManager.RemoveFromRoleAsync(
            await _userManager.FindByIdAsync(userId) ?? throw new ArgumentNullException(nameof(userId)), roleName);

    public async override Task<IList<string>> GetUserRolesAsync(string userId)
        => await _userManager.GetRolesAsync(
            await _userManager.FindByIdAsync(userId) ?? throw new ArgumentNullException(nameof(userId)));
}