using ISP.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace ISP.Domain.Entities.Identity;

public class Role : IdentityRole<string>, IBaseEntity
{
    public Role(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Role(string id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public string Description { get; set; }
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = "System";
    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}