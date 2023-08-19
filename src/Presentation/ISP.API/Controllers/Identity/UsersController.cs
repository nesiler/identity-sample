using ISP.Application.Requests.Identity.User;
using ISP.Application.Requests.Pagination;
using ISP.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NSwag.Annotations;

namespace ISP.API.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly IUserService _userManager;

    public UsersController(IMemoryCache memoryCache, IUserService userService)
    {
        _userManager = userService;
        _memoryCache = memoryCache;
    }

    [HttpGet("stats")]
    public ActionResult<MemoryCacheStatistics> GetStats() => Ok(_memoryCache.GetCurrentStatistics());

    [HttpGet("me")]
    [OpenApiOperation("Get the current user", "Need to be authenticated")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _userManager.GetUserAsync(User);
        return Ok(result);
    }

    [HttpGet("count")]
    [OpenApiOperation("Get the number of users", "No need to be authenticated")]
    public async Task<IActionResult> GetUsersCount()
    {
        var result = await _userManager.GetCountAsync();
        return Ok(result);
    }

    [HttpGet("user")]
    [OpenApiOperation("Get a user by id", "Need to be authenticated")]
    public async Task<IActionResult> GetUserById([FromQuery] string id)
    {
        var result = await _userManager.GetByIdAsync(id);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("all")]
    [OpenApiOperation("Get a list of all users", "Need to be authenticated")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userManager.GetListAsync();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation("Get a list of all users with pagination", "Need to be authenticated")]
    public async Task<IActionResult> GetUsers([FromQuery] Pagination pagination)
    {
        var result = await _userManager.GetListAsync(pagination);
        return Ok(result);
    }

    [HttpPost]
    [OpenApiOperation("Create a new user", "No need to be authenticated")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var result = await _userManager.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation("Update an existing user", "No need to be authenticated")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var result = await _userManager.UpdateAsync(request);
        return Ok(result);
    }

    [HttpDelete]
    [OpenApiOperation("Delete an existing user", "No need to be authenticated")]
    public async Task<IActionResult> DeleteUser([FromBody] string id)
    {
        var result = await _userManager.DeleteAsync(id);
        return Ok(result);
    }
}