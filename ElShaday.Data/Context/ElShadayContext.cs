using ElShaday.Domain.Entities;
using ElShaday.Domain.Entities.Department;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Context;

public class ElShadayContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<PhysicalPerson> PhysicalPeople { get; set; }
    public DbSet<LegalPerson> LegalPeople { get; set; }
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