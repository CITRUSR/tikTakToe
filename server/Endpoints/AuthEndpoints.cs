using Microsoft.AspNetCore.Mvc;
using server.Application.Contracts.Services;
using server.Application.Services.Auth.Dtos.Requests.LoginUser;
using server.Application.Services.Auth.Dtos.Requests.RefreshUserToken;
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

        builder
            .MapPost(
                "/api/auth/login",
                async (
                    [FromServices] IAuthService authService,
                    [FromBody] LoginUserRequest request
                ) =>
                {
                    var authResponse = await authService.LoginAsync(request);

                    return Results.Ok(authResponse);
                }
            )
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces<List<string>>(StatusCodes.Status400BadRequest)
            .WithTags(AUTH_TAG)
            .WithSummary("Login user")
            .WithDescription("Login user");

        builder
            .MapPost(
                "/api/auth/refresh",
                async (
                    [FromServices] IAuthService authService,
                    [FromBody] RefreshUserTokenRequest request
                ) =>
                {
                    var authResponse = await authService.RefreshAsync(request);

                    return Results.Ok(authResponse);
                }
            )
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces<List<string>>(StatusCodes.Status400BadRequest)
            .WithTags(AUTH_TAG)
            .WithSummary("Refresh tokens")
            .WithDescription("Refresh access and refresh token");

        return builder;
    }
}
