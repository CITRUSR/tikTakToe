using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore.Http;
using server.Application.Common.Factories;
using server.Application.Contracts.Providers;
using server.Application.Contracts.Services;
using server.Application.Providers;
using server.Application.Services.Auth;
using server.Application.Services.GameSession;
using server.Application.Services.GameSession.Handlers;
using server.Application.Services.Map;
using server.Application.Services.Room;
using server.Application.Services.RoomManager;
using server.Application.Services.User;
using server.Application.Services.UserStat;
using server.Handlers.Sockets;

namespace server.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ApplicationMapsterConfig.Configure();
        ConfigureFluentValidation(services);
        ConfigureServices(services);

        return services;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<ITokenProvider, TokenProvider>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IUserStatService, UserStatService>();

        services.AddScoped<IRoomService, RoomService>();

        services.AddScoped<IMapService, MapService>();

        services.AddSingleton<ISocketHandler, SocketHandler>();

        services.AddSingleton<IUserMessagesNotifier, UserMessagesNotifier>();

        services.AddSingleton<IGameSessionSocketHelper, GameSessionSocketHelper>();
    }

    private static void ConfigureFluentValidation(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<
            IFluentValidationEndpointFilterResultsFactory,
            FluentValidationFilterResultsFactory
        >();
    }
}
