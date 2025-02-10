using server.Application;
using server.Infrastructure;

namespace server.Extensions;

public static class StartupExtension
{
    public static void ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddOpenApi();
        services.AddInfrastructure(configuration);
        services.AddApplication();
    }

    public static void ConfigureApp(this WebApplication app)
    {
        app.UseWebSockets();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
    }
}
