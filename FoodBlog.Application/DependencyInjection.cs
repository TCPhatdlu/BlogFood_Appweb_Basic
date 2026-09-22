using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FoodBlog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(
            cfg => { },
            Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());

        return services;
    }
}