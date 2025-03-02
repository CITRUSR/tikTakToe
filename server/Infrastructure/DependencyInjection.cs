using System.Data;
using System.Data.Common;
using System.Reflection;
using DbUp;
using server.Application.Contracts.Repositories;
using server.Application.Options;
using server.Infrastructure.Factories;
using server.Infrastructure.Options;
using server.Infrastructure.Repositories;
using server.Infrastructure.Storages;
using server.Infrastructure.Utils.Constraints.Unique;

namespace server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        ConfigureDb(configuration);
        ConfigureServices(services, configuration);

        return services;
    }

    private static void ConfigureDb(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        if (connectionString == null)
        {
            throw new InvalidOperationException("Connection string is not set.");
        }

        var upgrader = DeployChanges
            .To.PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IDbConnection, DbConnection>(
            (sp) =>
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();

                return factory.CreateConnection();
            }
        );

        services.AddSingleton<IRoomStorage, RoomStorage>();

        services.AddSingleton<IUniqueConstraintChecker, PostgresUniqueConstraintChecker>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserStatRepository, UserStatRepository>();
        services.AddScoped<IRoomRepository, LocalRoomRepository>();

        services.AddSingleton<IGameSessionRepository, LocalGameSessionRepository>();

        services.Configure<AuthOptionsConfig>(configuration.GetSection("Jwt"));
        services.AddSingleton<IAuthOptions, AuthOptions>();
    }
}
