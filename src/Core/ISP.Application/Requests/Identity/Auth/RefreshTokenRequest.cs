namespace ISP.Application.Requests.Identity.Auth;

public record RefreshTokenRequest(string Token, string RefreshToken);