namespace ISP.Application.Requests.Identity.Role;

public sealed class UserRoleRequest
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
}