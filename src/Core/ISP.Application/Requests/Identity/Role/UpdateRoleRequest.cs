namespace ISP.Application.Requests.Identity.Role;

public sealed class UpdateRoleRequest
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}