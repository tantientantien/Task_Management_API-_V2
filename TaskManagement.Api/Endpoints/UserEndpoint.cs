using AutoMapper;
using Microsoft.AspNetCore.Identity;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/me", async (IMapper mapper, UserManagement userManagement) =>
        {
            var user = await userManagement.GetCurrentUserAsync();
            if (user is null)
                return Results.NotFound();

            var roles = await userManagement.GetCurrentUserRolesAsync();
            var userDto = mapper.Map<UserDataDto>(user);
            userDto.Roles = roles;

            return Results.Ok(SuccessResponse<UserDataDto>.Create(userDto));
        });

        group.MapPost("/logout", async (SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        });

        group.MapGet("/", async (IMapper mapper, UserManagement userManagement) =>
        {
            var users = await userManagement.GetAllUserAsync();
            if(users is null)
                return Results.NoContent();
            var userDtos = mapper.Map<IEnumerable<UserDataDto>>(users);
            return Results.Ok(SuccessResponse<IEnumerable<UserDataDto>>.Create(userDtos));
        });

        return group;
    }
}
