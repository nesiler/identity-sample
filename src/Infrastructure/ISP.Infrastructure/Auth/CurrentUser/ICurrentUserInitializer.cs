using System.Security.Claims;

namespace ISP.Infrastructure.Auth.CurrentUser;

public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}