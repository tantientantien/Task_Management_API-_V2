using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using TaskManagement.Api.Service;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/me", async (IMapper mapper, GetCurrentUser currentUser) =>
        {
            var user = await currentUser.GetCurrentUserAsync();
            if (user is null)
                return Results.NotFound();

            var roles = await currentUser.GetCurrentUserRolesAsync();
            var userDto = mapper.Map<UserDataDto>(user);
            userDto.Roles = roles;

            return Results.Ok(SuccessResponse<UserDataDto>.Create(userDto));
        });

        group.MapPost("/logout", async (SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        });

        return group;
    }
}
