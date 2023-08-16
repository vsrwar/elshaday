using ElShaday.Application.Interfaces;
using ElShaday.Application.Mappings;
using ElShaday.Application.Services;
using ElShaday.Data.Context;
using ElShaday.Data.Repositories;
using ElShaday.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElShaday.Crosscutting.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDataBaseConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ElShadayContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdminUserRepository, AdminUserRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminUserService, AdminUserService>();
        return services;
    }
    
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
        return services;
    }
}