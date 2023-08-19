using System.Security.Claims;

namespace ISP.Domain.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimTypes.Email);

    public static string? GetFullName(this ClaimsPrincipal principal)
        => principal?.FindFirst(ISPClaims.Fullname)?.Value;

    public static string? GetFirstName(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Name)?.Value;

    public static string? GetSurname(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Surname)?.Value;

    public static string? GetPhoneNumber(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimTypes.MobilePhone);

    public static string? GetUserId(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimTypes.NameIdentifier);

    public static string? GetRole(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Role)?.Value;

    public static string? GetOffice(this ClaimsPrincipal principal)
        => principal?.FindFirst(ISPClaims.Office)?.Value;

    public static string? GetIpAddress(this ClaimsPrincipal principal)
        => principal?.FindFirst(ISPClaims.IpAddress)?.Value;

    public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal) =>
        DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(
            principal.FindFirstValue(ISPClaims.Expiration)));


    private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value;
}