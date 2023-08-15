using ElShaday.Domain.Entities;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Context;

public class ElShadayContext : DbContext
{
    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<CommonUser> CommonUsers { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<CustomerPhysicalPerson> CustomerPhysicalPersons { get; set; }
    public DbSet<CustomerLegalPerson> CustomerLegalPersons { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public ElShadayContext(DbContextOptions<ElShadayContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ElShadayContext).Assembly);
    }
}