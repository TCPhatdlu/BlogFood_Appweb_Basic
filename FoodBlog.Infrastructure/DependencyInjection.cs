using Microsoft.Extensions.DependencyInjection;

namespace FoodBlog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Sẽ đăng ký DbContext, Repositories ở các bước sau
        return services;
    }
}