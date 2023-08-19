using ISP.Application.Requests.Identity.User;
using ISP.Application.Requests.Pagination;
using ISP.Application.ViewModels.User;
using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ISP.Application.Services;

public abstract class IUserService : UserManager<User>
{
    protected IUserService(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store,
        optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public abstract Task<IList<UserDto>> GetListAsync(Pagination pagination);

    public abstract Task<IList<UserDto>> GetListAsync();

    public abstract Task<UserDto> GetByIdAsync(string id);

    public abstract Task<IdentityResult> CreateAsync(CreateUserRequest request);

    public abstract Task<IdentityResult> UpdateAsync(UpdateUserRequest request);

    public abstract Task<IdentityResult> DeleteAsync(string id);

    public abstract Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber);

    public abstract Task<bool> ExistsWithEmailAsync(string email);

    public abstract Task<bool> ExistsWithUserNameAsync(string userName);

    public abstract Task<User?> FindByPhoneNumberAsync(string phoneNumber);

    public abstract Task<User?> FindByUserNameAsync(string userName);

    public abstract Task<int> GetCountAsync();
}