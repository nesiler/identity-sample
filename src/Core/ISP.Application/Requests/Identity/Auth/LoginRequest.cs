namespace ISP.Application.Requests.Identity.Auth;

public sealed class LoginRequest
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}