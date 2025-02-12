using Microsoft.AspNetCore.Mvc;
using server.Application.Contracts.Services;
using server.Application.Services.User.Dtos.Responses;

namespace server.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder Map(this IEndpointRouteBuilder builder)
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

        return builder;
    }
}
