using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Users;
using Service.Users;

namespace Api.Configuration;

public static class ServiceConfiguration
{
    public static async Task<WebApplication> UseMigrationAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            await db.Database.MigrateAsync();
        }

        return app;
    }

    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddServices(services);

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
