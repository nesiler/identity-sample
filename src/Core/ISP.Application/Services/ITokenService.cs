using ISP.Application.Requests.Identity.Auth;
using ISP.Application.ViewModels.Role;

namespace ISP.Application.Services;

public interface ITokenService
{
    Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress);

    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
}