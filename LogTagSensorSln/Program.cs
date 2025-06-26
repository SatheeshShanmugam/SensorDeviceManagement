using LogTagSensor.Api.Middleware;
using LogTagSensor.Application.Extensions;
using LogTagSensor.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Configuration.AddConfiguration(config);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationLayerDIs();
builder.Services.AddInfrastructureServices(config);
builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogTagSensor API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
