using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using server.Application;
using server.Application.Options;
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
        ConfigureJwt(services);
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
        var root = builder.MapGroup("/").AddFluentValidationFilter();

        UserEndpoints.Map(root);
        AuthEndpoints.Map(root);
        UserStatEndpoints.Map(root);
    }

    private static void ConfigureJwt(IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var authOptions = services
                    .BuildServiceProvider()
                    .GetRequiredService<IAuthOptions>();

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authOptions.Issuer,
                    ValidateIssuer = true,

                    ValidAudience = authOptions.Audience,
                    ValidateAudience = true,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    }
}
