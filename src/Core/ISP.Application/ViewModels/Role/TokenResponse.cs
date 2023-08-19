namespace ISP.Application.ViewModels.Role;

public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);