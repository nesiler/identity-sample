using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ISP.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ISP.Application.Validators.Identity;

public class UserValidator : IUserValidator<User>
{
    private readonly ErrorDescriber errorDescriber;

    public UserValidator(ErrorDescriber errorDescriber)
    {
        this.errorDescriber = errorDescriber;
    }

    public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var errors = new List<IdentityError>();

        // Isim doğrulaması
        if (string.IsNullOrWhiteSpace(user.FirstName))
            errors.Add(errorDescriber.EmptyFirstName());
        else if (user.FirstName.Length > 50)
            errors.Add(errorDescriber.InvalidFirstNameLength(50));
        else if (user.FirstName.Length < 2)
            errors.Add(errorDescriber.InvalidFirstNameLength(2));
        else if (!Regex.IsMatch(user.FirstName, @"^[a-zA-Z]+$"))
            errors.Add(errorDescriber.InvalidFirstName(user.FirstName));

        // Soyisim doğrulaması
        if (string.IsNullOrWhiteSpace(user.LastName))
            errors.Add(errorDescriber.EmptyLastName());
        else if (user.LastName.Length > 50)
            errors.Add(errorDescriber.InvalidLastNameLength(50));
        else if (user.LastName.Length < 2)
            errors.Add(errorDescriber.InvalidLastNameLength(2));
        else if (!Regex.IsMatch(user.LastName, @"^[a-zA-Z]+$"))
            errors.Add(errorDescriber.InvalidLastName(user.LastName));

        // Email doğrulaması
        if (string.IsNullOrWhiteSpace(user.Email))
            errors.Add(errorDescriber.EmptyEmail());
        else if (!new EmailAddressAttribute().IsValid(user.Email))
            errors.Add(errorDescriber.InvalidEmail(user.Email));


        // Telefon doğrulaması
        if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            if (user.PhoneNumber.Length > 15)
                errors.Add(errorDescriber.InvalidPhoneNumberLength(15));
            else if (!Regex.IsMatch(user.PhoneNumber, @"^[0-9]+$"))
                errors.Add(errorDescriber.InvalidPhoneNumber());
        }

        // Return the result
        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }
}