using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ISP.Application.Validators.Identity;

public class PasswordValidator : IPasswordValidator<User>
{
    public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        var errors = new List<IdentityError>();
        var errorDescriber = new ErrorDescriber();

        // Validate password
        if (string.IsNullOrWhiteSpace(password))
            errors.Add(errorDescriber.PasswordCanNotBeNull());

        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }
}