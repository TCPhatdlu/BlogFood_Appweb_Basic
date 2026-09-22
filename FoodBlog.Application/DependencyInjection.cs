using Microsoft.Extensions.DependencyInjection;

namespace FoodBlog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Sẽ đăng ký MediatR, AutoMapper, FluentValidation ở các bước sau
        return services;
    }
}