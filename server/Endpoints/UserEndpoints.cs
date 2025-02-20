using Microsoft.AspNetCore.Mvc;
using server.Application.Contracts.Services;
using server.Application.Services.User.Dtos.Requests.GetUserById;
using server.Application.Services.User.Dtos.Requests.GetUserByNickname;
using server.Application.Services.User.Dtos.Requests.UpdateUser;
using server.Application.Services.User.Dtos.Responses;

namespace server.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder Map(this RouteGroupBuilder builder)
    {
        const string USER_TAG = "User";

        builder
            .MapGet(
                "/api/users",
                async ([FromServices] IUserService userService) =>
                {
                    var users = await userService.GetAllUsersAsync();

                    return Results.Ok(users);
                }
            )
            .Produces<List<UserViewModel>>(StatusCodes.Status200OK)
            .WithTags(USER_TAG)
            .WithSummary("Get all users")
            .WithDescription("Get all users");

        builder
            .MapGet(
                "/api/user/byid",
                async (
                    [FromServices] IUserService userService,
                    [AsParameters] GetUserByIdRequest request
                ) =>
                {
                    var user = await userService.GetUserAsync(request);

                    return Results.Ok(user);
                }
            )
            .Produces<UserDto>(StatusCodes.Status200OK)
            .WithTags(USER_TAG)
            .WithSummary("Get user by id")
            .WithDescription("Get user by id");

        builder
            .MapGet(
                "/api/user/bynickname",
                async (
                    [FromServices] IUserService userService,
                    [AsParameters] GetUserByNicknameRequest request
                ) =>
                {
                    var user = await userService.GetUserAsync(request);

                    return Results.Ok(user);
                }
            )
            .Produces<UserDto>(StatusCodes.Status200OK)
            .WithTags(USER_TAG)
            .WithSummary("Get user by nickname")
            .WithDescription("Get user by nickname");

        builder
            .MapPut(
                "/api/user",
                async (
                    [FromServices] IUserService userService,
                    [FromBody] UpdateUserRequest request
                ) =>
                {
                    var user = await userService.UpdateUserAsync(request);

                    return Results.Ok(user);
                }
            )
            .Produces<UserDto>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(USER_TAG)
            .WithSummary("Update user")
            .WithDescription("Update user");

        return builder;
    }
}
