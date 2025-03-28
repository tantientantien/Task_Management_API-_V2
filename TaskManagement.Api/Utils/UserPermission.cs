using Microsoft.AspNetCore.Identity;


public class UserPermission
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly UserManagement _userManagement;

    public UserPermission(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, UserManagement userManagement)
    {
        _userManager = userManager;
        _userManagement = userManagement;
    }

    public async Task<bool> isAdminOrCreator(int userId)
    {
        var currentUser = await _userManagement.GetCurrentUserAsync();
        if (currentUser == null)
        {
            return false;
        }

        var userRoles = await _userManagement.GetCurrentUserRolesAsync();

        if (userRoles.Contains("Admin") || currentUser.Id == userId)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> isAdminOrCreatorOrAssignee(int userId, int assigneeId)
    {
        var currentUser = await _userManagement.GetCurrentUserAsync();
        if (currentUser == null)
        {
            return false;
        }

        var userRoles = await _userManagement.GetCurrentUserRolesAsync();

        if (userRoles.Contains("Admin") || currentUser.Id == userId || currentUser.Id == assigneeId)
        {
            return true;
        }

        return false;
    }


}
