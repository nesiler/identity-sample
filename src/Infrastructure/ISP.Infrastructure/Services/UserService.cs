using AutoMapper;
using ISP.Application.Repositories.Contracts;
using ISP.Application.Requests.Identity.User;
using ISP.Application.Requests.Pagination;
using ISP.Application.Services;
using ISP.Application.Validators.Identity;
using ISP.Application.ViewModels.User;
using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ISP.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ErrorDescriber _errorDescriber;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(ErrorDescriber errorDescriber, IMapper mapper, IUserRepository userRepository,
        IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services,
        ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators,
        passwordValidators, keyNormalizer, errors, services, logger)
    {
        _errorDescriber = errorDescriber;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async override Task<IdentityResult> CreateAsync(CreateUserRequest request)
    {
        var user = _mapper.Map<User>(request);
        user.UserName = user.Email.Split('@')[0] + DateTime.Today.ToString("ddMM");

        //check if user exists
        if (await _userRepository.GetSingleAsync(u => u.Email == user.Email) != null)
            return IdentityResult.Failed(_errorDescriber.DuplicateEmail(request.Email));

        //check username exists, if it exists set another one
        while (await _userRepository.GetSingleAsync(u => u.UserName == user.UserName) is not null)
            user.UserName = user.UserName.Substring(0, user.UserName.Length - 1) +
                            (int.Parse(user.UserName.Substring(user.UserName.Length - 1)) + 1);

        return await base.CreateAsync(user);
    }

    public async override Task<IdentityResult> UpdateAsync(UpdateUserRequest request)
    {
        var userToUpdate = await base.FindByIdAsync(request.Id);
        _mapper.Map(request, userToUpdate);
        await base.UpdateSecurityStampAsync(userToUpdate);
        return await base.UpdateAsync(userToUpdate);
    }

    public async override Task<IdentityResult> DeleteAsync(string id)
    {
        var userToDelete = await base.FindByIdAsync(id);
        if (userToDelete is null)
            return IdentityResult.Failed(_errorDescriber.UserNotFound());
        return await base.DeleteAsync(userToDelete);
    }

    public async override Task<IList<UserDto>> GetListAsync(Pagination pagination)
    {
        var users = await _userRepository
            .GetAll()
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return _mapper.Map<List<UserDto>>(users);
    }

    public async override Task<IList<UserDto>> GetListAsync()
    {
        var users = await _userRepository
            .GetAll()
            .ToListAsync();
        return _mapper.Map<List<UserDto>>(users).ToList();
    }

    public async override Task<UserDto> GetByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async override Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber) =>
        await _userRepository.GetSingleAsync(u => u.PhoneNumber == phoneNumber) != null;

    public async override Task<bool> ExistsWithEmailAsync(string email) =>
        await _userRepository.GetSingleAsync(u => u.Email == email) != null;

    public async override Task<bool> ExistsWithUserNameAsync(string userName) =>
        await _userRepository.GetSingleAsync(u => u.UserName == userName) != null;

    public async override Task<User?> FindByPhoneNumberAsync(string phoneNumber) =>
        await _userRepository.GetSingleAsync(u => u.PhoneNumber == phoneNumber);

    public async override Task<User?> FindByUserNameAsync(string userName) =>
        await _userRepository.GetSingleAsync(u => u.UserName == userName);

    public async override Task<int> GetCountAsync() => await _userRepository.GetCountAsync();
}