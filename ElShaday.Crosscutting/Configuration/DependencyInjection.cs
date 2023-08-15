using ElShaday.Data.Context;
using ElShaday.Data.Repositories;
using ElShaday.Domain.Entities;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Entities.User;
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
        services.AddScoped<IRepository<AdminUser>, Repository<AdminUser>>();
        services.AddScoped<IRepository<CommonUser>, Repository<CommonUser>>();
        services.AddScoped<IRepository<Department>, Repository<Department>>();
        services.AddScoped<IRepository<Supplier>, Repository<Supplier>>();
        services.AddScoped<IRepository<Employee>, Repository<Employee>>();
        services.AddScoped<IRepository<CustomerPhysicalPerson>, Repository<CustomerPhysicalPerson>>();
        services.AddScoped<IRepository<CustomerLegalPerson>, Repository<CustomerLegalPerson>>();
        services.AddScoped<IRepository<Address>, Repository<Address>>();
        return services;
    }
}