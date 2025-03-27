using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Api.Service;

public class UserPermission
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly GetCurrentUser _currentUser;

    public UserPermission(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, GetCurrentUser currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public async Task<bool> isAdminOrCreator(int userId)
    {
        var currentUser = await _currentUser.GetCurrentUserAsync();
        if (currentUser == null)
        {
            return false;
        }

        var userRoles = await _currentUser.GetCurrentUserRolesAsync();

        if (userRoles.Contains("Admin") && currentUser.Id == userId)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> isAdminOrCreatorOrAssignee(int userId, int assigneeId)
    {
        var currentUser = await _currentUser.GetCurrentUserAsync();
        if (currentUser == null)
        {
            return false;
        }

        var userRoles = await _currentUser.GetCurrentUserRolesAsync();

        if (userRoles.Contains("Admin") && currentUser.Id == userId && currentUser.Id == assigneeId)
        {
            return true;
        }

        return false;
    }


//         public async Task<IActionResult> CheckUserPermission(UserManager<User> userManager, HttpContext httpContext, int storedTaskUserId)
// {
//     // Retrieve the current user's ID
//     var userIdString = userManager.GetUserId(httpContext.User);
//     if (!int.TryParse(userIdString, out int userId))
//     {
//         return new UnauthorizedObjectResult(new { status = "error", message = "Authentication required" });
//     }

//     // Check if the user is an admin
//     var isAdmin = await userManager.IsInRoleAsync(await userManager.FindByIdAsync(userIdString), "Admin");

//     // Check if the user is the creator or an admin
//     if (!isAdmin && userId != storedTaskUserId)
//     {
//         return new ForbidResult();
//     }

//     return new OkResult(); // User is authorized
// }

}