using Microsoft.EntityFrameworkCore;

namespace FoodBlog.Infrastructure.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	// Sẽ thêm DbSet cho các Entity sau
}