using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


public class UserManagement
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManagement(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<User> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return null;
        }

        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<List<User>> GetAllUserAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public int GetCurrentUserId()
    {
        return int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    public async Task<IList<string>> GetCurrentUserRolesAsync()
    {
        var user = await GetCurrentUserAsync();
        if (user == null)
        {
            return new List<string>();
        }

        return await _userManager.GetRolesAsync(user);
    }

}