using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore.Http;
using server.Application.Common.Factories;
using server.Application.Contracts.Providers;
using server.Application.Contracts.Services;
using server.Application.Providers;
using server.Application.Services.Auth;
using server.Application.Services.User;

namespace server.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ConfigureFluentValidation(services);
        ConfigureServices(services);

        return services;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<ITokenProvider, TokenProvider>();

        services.AddScoped<IAuthService, AuthService>();
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
