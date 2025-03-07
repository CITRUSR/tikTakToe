using server.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddFluentValidationEndpointFilter();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureApp();

app.Run();
