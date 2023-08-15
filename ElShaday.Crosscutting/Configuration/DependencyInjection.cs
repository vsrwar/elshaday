using ElShaday.Application.Interfaces;
using ElShaday.Application.Services;
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

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICrudService<AdminUser>, CrudService<AdminUser>>();
        services.AddScoped<ICrudService<CommonUser>, CrudService<CommonUser>>();
        services.AddScoped<ICrudService<Department>, CrudService<Department>>();
        services.AddScoped<ICrudService<Supplier>, CrudService<Supplier>>();
        services.AddScoped<ICrudService<Employee>, CrudService<Employee>>();
        services.AddScoped<ICrudService<CustomerPhysicalPerson>, CrudService<CustomerPhysicalPerson>>();
        services.AddScoped<ICrudService<CustomerLegalPerson>, CrudService<CustomerLegalPerson>>();
        services.AddScoped<ICrudService<Address>, CrudService<Address>>();
        return services;
    }
}