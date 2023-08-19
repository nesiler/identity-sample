using ISP.Application.Requests.Identity.Role;
using ISP.Application.Services;
using ISP.Application.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace ISP.API.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [OpenApiOperation("Get a list of all roles.", "")]
    public Task<List<RoleDto>> GetListAsync() =>
        _roleService.GetListAsync();

    [HttpGet("id/")]
    [OpenApiOperation("Get role details.", "")]
    public async Task<RoleDto> GetByIdAsync([FromQuery] string id) => await _roleService.GetByIdAsync(id);

    [HttpPost]
    [OpenApiOperation("Create new role.", "")]
    public async Task<IdentityResult> CreateRoleAsync([FromBody] CreateRoleRequest request) =>
        await _roleService.CreateAsync(request);

    [HttpPut]
    [OpenApiOperation("Update a role.", "")]
    public async Task<IdentityResult> UpdateRoleAsync([FromBody] UpdateRoleRequest request) =>
        await _roleService.UpdateAsync(request);

    [HttpPost("initialize")]
    [OpenApiOperation("Initialize.", "")]
    public async Task<string> InitializeAsync() => await _roleService.InitializeAsync() ? "Initialized" : "Failed";

    [HttpDelete]
    [OpenApiOperation("Delete a role.", "")]
    public async Task<IdentityResult> DeleteAsync([FromQuery] string id) => await _roleService.DeleteAsync(id);

    [HttpPost("add-user-role")]
    [OpenApiOperation("Assign a user to a role.", "")]
    public async Task AddUserRole([FromBody] UserRoleRequest request) =>
        await _roleService.AddUserToRoleAsync(request.UserId, request.RoleName);

    [HttpDelete("remove-user-role")]
    [OpenApiOperation("Remove a user from a role.", "")]
    public async Task RemoveUserRole([FromBody] UserRoleRequest request) =>
        await _roleService.RemoveUserFromRoleAsync(request.UserId, request.RoleName);

    [HttpGet("user-roles")]
    [OpenApiOperation("Get a list of user roles.", "")]
    public async Task<IList<string>> GetUserRolesAsync([FromQuery] string userId) =>
        await _roleService.GetUserRolesAsync(userId);
}