namespace server.Extensions;

public static class StartupExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddOpenApi();
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
