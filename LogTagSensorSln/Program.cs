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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //swagger configuration can be added here if needed
}

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
