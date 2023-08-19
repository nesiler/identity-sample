using System.Security.Claims;
using ISP.Application.Exceptions;
using ISP.Domain.Entities.Common;

namespace ISP.Infrastructure.Auth.CurrentUser;

public class CurrentUser : ICurrentUser, ICurrentUserInitializer
{
    private ClaimsPrincipal? _user;

    private string _userId = Guid.Empty.ToString();

    public string? Name => _user?.Identity?.Name;

    public string GetUserId() =>
        IsAuthenticated()
            ? _user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString()
            : _userId;

    public string? GetUserEmail() =>
        IsAuthenticated()
            ? _user!.FindFirstValue(ClaimTypes.Email)
            : string.Empty;

    public bool IsAuthenticated() =>
        _user?.Identity?.IsAuthenticated is true;

    public bool IsInRole(string role) =>
        _user?.IsInRole(role) is true;

    public IEnumerable<Claim>? GetUserClaims() =>
        _user?.Claims;


    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null) throw new ConflictException("User is already set.");

        _user = user;
    }

    public void SetCurrentUserId(string userId)
    {
        if (_userId != string.Empty) throw new ConflictException("User id is already set.");

        if (!string.IsNullOrEmpty(userId)) _userId = userId;
    }
}