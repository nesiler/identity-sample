using System.Collections.ObjectModel;

namespace ISP.Domain.Authorization;

public static class ISPRoles
{
    public const string Admin = nameof(Admin);
    public const string Developer = nameof(Developer);
    public const string Manager = nameof(Manager);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        Developer,
        Manager
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}