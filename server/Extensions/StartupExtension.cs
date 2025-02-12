using server.Application;
using server.Endpoints;
using server.Infrastructure;
using server.Middlewares;

namespace server.Extensions;

public static class StartupExtension
{
    public static void ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddInfrastructure(configuration);
        services.AddApplication();
    }

    public static void ConfigureApp(this WebApplication app)
    {
        app.UseWebSockets();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        MapEndpoints(app);

        app.UseMiddleware<GlobalExceptionHandler>();
    }

    private static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        UserEndpoints.Map(builder);
    }
}
