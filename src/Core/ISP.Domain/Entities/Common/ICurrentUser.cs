using System.Security.Claims;

namespace ISP.Domain.Entities.Common;

public interface ICurrentUser
{
    string? Name { get; }

    string GetUserId();

    string? GetUserEmail();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();
}