using ISP.Application.Requests.Identity.Auth;
using ISP.Application.Services;
using ISP.Application.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace ISP.API.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public sealed class TokensController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokensController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [OpenApiOperation("Request an access token using credentials.", "")]
    public Task<TokenResponse> GetTokenAsync(TokenRequest request) =>
        _tokenService.GetTokenAsync(request, GetIpAddress()!);

    [HttpPost("refresh")]
    [OpenApiOperation("Request an access token using a refresh token.", "")]
    public Task<TokenResponse> RefreshAsync(RefreshTokenRequest request) =>
        _tokenService.RefreshTokenAsync(request, GetIpAddress()!);

    private string? GetIpAddress() =>
        Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"]
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
}