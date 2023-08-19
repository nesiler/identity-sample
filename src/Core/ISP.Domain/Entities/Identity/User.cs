using ISP.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace ISP.Domain.Entities.Identity;

public class User : IdentityUser<string>, IBaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = "System";
    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}