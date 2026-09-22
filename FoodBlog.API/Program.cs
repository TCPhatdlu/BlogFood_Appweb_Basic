using FoodBlog.Application;
using FoodBlog.Infrastructure;
using FoodBlog.Infrastructure.Data;
using Serilog;

// Cấu hình Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting FoodBlog API");

    var builder = WebApplication.CreateBuilder(args);

    // Thêm Serilog vào logging pipeline
    builder.Host.UseSerilog();

    // Cấu hình DI theo Clean Architecture
    builder.Services.AddApplicationServices();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddInfrastructureServices(connectionString!);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}