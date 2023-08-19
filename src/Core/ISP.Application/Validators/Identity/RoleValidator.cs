using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ISP.Application.Validators.Identity;

public class RoleValidator : IRoleValidator<Role>
{
    private readonly ErrorDescriber _errorDescriber;

    public RoleValidator(ErrorDescriber errorDescriber)
    {
        _errorDescriber = errorDescriber;
    }

    public async Task<IdentityResult> ValidateAsync(RoleManager<Role> roleManager, Role role)
    {
        var errors = new List<IdentityError>();

        if (await roleManager.RoleExistsAsync(role.Name))
            errors.Add(_errorDescriber.RoleAlreadyExists());

        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }
}