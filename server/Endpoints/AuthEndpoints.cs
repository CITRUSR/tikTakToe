using Microsoft.AspNetCore.Mvc;
using server.Application.Contracts.Services;
using server.Application.Services.Auth.Dtos.Responses;

namespace server.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder Map(this RouteGroupBuilder builder)
    {
        const string AUTH_TAG = "Auth";

        builder
            .MapPost(
                "/api/auth/register",
                async (
                    [FromServices] IAuthService authService,
                    [FromBody] RegisterUserRequest request
                ) =>
                {
                    var authResponse = await authService.RegisterAsync(request);

                    return Results.Created("", authResponse);
                }
            )
            .Produces<AuthResponse>(StatusCodes.Status201Created)
            .Produces<List<string>>(StatusCodes.Status400BadRequest)
            .WithTags(AUTH_TAG)
            .WithSummary("Register a new user")
            .WithDescription("Register a new user");

        return builder;
    }
}
