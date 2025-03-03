using Microsoft.AspNetCore.Mvc;
using server.Application.Contracts.Services;
using server.Application.Services.UserStat.Dtos.Requests.GetUserStat;
using server.Application.Services.UserStat.Dtos.Requests.UpdateUserStat;
using server.Application.Services.UserStat.Dtos.Responses;

namespace server.Endpoints;

public static class UserStatEndpoints
{
    public static IEndpointRouteBuilder Map(RouteGroupBuilder builder)
    {
        const string USER_STAT_TAG = "UserStat";

        builder
            .MapGet(
                "/api/userStat",
                async (
                    [AsParameters] GetUserStatRequest request,
                    [FromServices] IUserStatService userStatService
                ) =>
                {
                    var userStat = await userStatService.GetAsync(request);

                    return Results.Ok(userStat);
                }
            )
            .Produces<UserStatDto>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(USER_STAT_TAG)
            .WithSummary("Get user statistics")
            .WithDescription("Get user statistics");

        builder
            .MapPut(
                "/api/userStat",
                async (
                    [FromBody] UpdateUserStatRequest request,
                    [FromServices] IUserStatService userStatService
                ) =>
                {
                    var userStat = await userStatService.UpdateAsync(request);

                    return Results.Ok(userStat);
                }
            )
            .Produces<UserStatDto>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithTags(USER_STAT_TAG)
            .WithSummary("Update user statistics")
            .WithDescription("Update user statistics");

        return builder;
    }
}
