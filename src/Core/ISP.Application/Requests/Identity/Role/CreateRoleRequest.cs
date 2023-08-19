namespace ISP.Application.Requests.Identity.Role;

public sealed class CreateRoleRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}