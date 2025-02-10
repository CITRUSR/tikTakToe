namespace server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }

    private static void ConfigureDb(this IServiceCollection services) { }
}
