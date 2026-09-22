using FoodBlog.Application;
using FoodBlog.Infrastructure;
using FoodBlog.Infrastructure.Data;
using FoodBlog.Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore; // <-- This fixes the 'MigrateAsync' error
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting FoodBlog API");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    builder.Services.AddApplicationServices();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddInfrastructureServices(connectionString!);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // DEBUG: This will tell us exactly what environment the app thinks it's in
    Log.Information("CURRENT ENVIRONMENT: {Environment}", app.Environment.EnvironmentName);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        Log.Information("🚀 STARTING MIGRATION AND SEEDING...");

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // This works now because of "using Microsoft.EntityFrameworkCore;" at the top
            await dbContext.Database.MigrateAsync();

            // Gọi hàm seed dữ liệu
            await DatabaseSeeder.SeedAsync(dbContext);

            Log.Information("✅ SEEDING FINISHED SUCCESSFULLY.");
        }
    }
    else
    {
        Log.Warning("⚠️ SKIPPING SEEDING: Environment is NOT Development. It is {Environment}", app.Environment.EnvironmentName);
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