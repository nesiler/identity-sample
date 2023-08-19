using Microsoft.AspNetCore.Identity;

namespace ISP.Application.Validators.Identity;

public class ErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new() { Code = "DefaultError", Description = "Bir hata oluştu." };

    public override IdentityError ConcurrencyFailure() =>
        new() { Code = "ConcurrencyFailure", Description = "Eş zamanlılık hatası." };

    public IdentityError EmptyFirstName() => new()
    {
        Code = "EmptyFirstName",
        Description = "İsim alanı gereklidir."
    };

    //user not found
    public IdentityError UserNotFound() => new()
    {
        Code = "UserNotFound",
        Description = "Kullanıcı bulunamadı."
    };

    public IdentityError InvalidFirstNameLength(int length) => new()
    {
        Code = "InvalidFirstNameLength",
        Description = $"İsim alanı {length} karakterden uzun olmamalıdır."
    };

    public IdentityError InvalidFirstName(string firstName) => new()
    {
        Code = "InvalidFirstName",
        Description = "İsim alanı sadece harflerden oluşmalıdır."
    };

    // Soyisim hataları
    public IdentityError EmptyLastName() => new()
    {
        Code = "EmptyLastName",
        Description = "Soyisim alanı gereklidir."
    };

    public IdentityError InvalidLastNameLength(int length) => new()
    {
        Code = "InvalidLastNameLength",
        Description = $"Soyisim alanı {length} karakterden uzun olmamalıdır."
    };

    public IdentityError InvalidLastName(string lastName) => new()
    {
        Code = "InvalidLastName",
        Description = "Soyisim alanı sadece harflerden oluşmalıdır."
    };

    // E-posta hataları
    public IdentityError EmptyEmail() => new()
    {
        Code = "EmptyEmail",
        Description = "E-posta alanı gereklidir."
    };

    // Telefon numarası hataları
    public IdentityError InvalidPhoneNumberLength(int length) => new()
    {
        Code = "InvalidPhoneNumberLength",
        Description = $"Telefon numarası {length} karakterden uzun olmamalıdır."
    };

    public IdentityError InvalidPhoneNumber() => new()
    {
        Code = "InvalidPhoneNumber",
        Description = "Telefon numarası sadece rakamlardan oluşmalıdır."
    };

    public override IdentityError PasswordMismatch() =>
        new() { Code = "PasswordMismatch", Description = "Parola eşleşmiyor." };

    public override IdentityError InvalidToken() => new() { Code = "InvalidToken", Description = "Geçersiz token." };

    public override IdentityError RecoveryCodeRedemptionFailed() => new()
        { Code = "RecoveryCodeRedemptionFailed", Description = "Kurtarma kodu kullanılamadı." };

    public override IdentityError LoginAlreadyAssociated() => new()
        { Code = "LoginAlreadyAssociated", Description = "Giriş zaten ilişkilendirilmiş." };

    public override IdentityError InvalidUserName(string userName) =>
        new() { Code = "InvalidUserName", Description = "Geçersiz kullanıcı adı." };

    public override IdentityError InvalidEmail(string email) =>
        new() { Code = "InvalidEmail", Description = "Geçersiz e-posta." };

    public override IdentityError DuplicateUserName(string userName) => new()
        { Code = "DuplicateUserName", Description = $"\"{userName}\" kullanıcı adı kullanılmaktadır." };

    public override IdentityError DuplicateEmail(string email) => new()
        { Code = "DuplicateEmail", Description = $"\"{email}\" başka bir kullanıcı tarafından kullanılmaktadır." };

    public override IdentityError InvalidRoleName(string role) =>
        new() { Code = "InvalidRoleName", Description = "Geçersiz rol adı." };

    public override IdentityError DuplicateRoleName(string role) => new()
        { Code = "DuplicateRoleName", Description = "Aynı isimde başka bir rol zaten var." };

    public override IdentityError UserAlreadyHasPassword() => new()
        { Code = "UserAlreadyHasPassword", Description = "Kullanıcının zaten bir parolası var." };

    public override IdentityError UserLockoutNotEnabled() => new()
        { Code = "UserLockoutNotEnabled", Description = "Kullanıcı kilitleme etkinleştirilmemiş." };

    public override IdentityError UserAlreadyInRole(string role) => new()
        { Code = "UserAlreadyInRole", Description = $"Kullanıcı zaten \"{role}\" rolünde." };

    public override IdentityError UserNotInRole(string role) => new()
        { Code = "UserNotInRole", Description = $"Kullanıcı \"{role}\" rolünde değil." };

    public override IdentityError PasswordTooShort(int length) => new()
        { Code = "PasswordTooShort", Description = $"Parola en az {length} karakter uzunluğunda olmalıdır." };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new()
        { Code = "PasswordRequiresUniqueChars", Description = $"Parola en az {uniqueChars} farklı karakter içermelidir." };

    public override IdentityError PasswordRequiresNonAlphanumeric() => new()
    {
        Code = "PasswordRequiresNonAlphanumeric", Description = "Parola harf veya rakam dışında özel karakter içermelidir."
    };

    public override IdentityError PasswordRequiresDigit() => new()
        { Code = "PasswordRequiresDigit", Description = "Parola en az bir rakam içermelidir." };

    public override IdentityError PasswordRequiresLower() => new()
        { Code = "PasswordRequiresLower", Description = "Parola en az bir küçük harf içermelidir." };

    public override IdentityError PasswordRequiresUpper() => new()
        { Code = "PasswordRequiresUpper", Description = "Parola en az bir büyük harf içermelidir." };

    public IdentityError PasswordRequiresUniqueChars() => new()
        { Code = "PasswordRequiresUniqueChars", Description = "Parola en az bir özel karakter içermelidir." };

    public IdentityError PasswordCanNotBeNull() => new()
        { Code = "PasswordCanNotNull", Description = "Parola boş olamaz." };

    public IdentityError RoleAlreadyExists() => new() { Code = "RoleAlreadyExists", Description = "Rol zaten mevcut." };

    //now allowed to delete role
    public IdentityError RoleCanNotBeDeleted() =>
        new() { Code = "RoleCanNotBeDeleted", Description = "Bu rol silinemez. " };

    public IdentityError RoleHasUsers() => new() { Code = "RoleHasUsers", Description = "Rolde kullanıcılar mevcut." };

    //role not found
    public IdentityError RoleNotFound() => new() { Code = "RoleNotFound", Description = "Rol bulunamadı." };
}