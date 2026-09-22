using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FoodBlog.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // 1. Lấy đường dẫn hiện tại (thường là thư mục gốc solution)
        string currentDir = Directory.GetCurrentDirectory();

        // 2. Xây dựng đường dẫn đến appsettings.json của project API
        string configPath = Path.Combine(currentDir, "FoodBlog.API", "appsettings.json");

        // 3. Nếu không tìm thấy, thử lùi lại 1 cấp thư mục (phòng trường hợp chạy lệnh từ thư mục con)
        if (!File.Exists(configPath))
        {
            configPath = Path.Combine(currentDir, "..", "FoodBlog.API", "appsettings.json");
        }

        // 4. Đọc cấu hình
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(configPath)!)
            .AddJsonFile(Path.GetFileName(configPath), optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Không tìm thấy 'DefaultConnection' trong appsettings.json");

        // 5. Cấu hình DbContext
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}