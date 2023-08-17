using ElShaday.Data.Context;
using ElShaday.Domain.Entities.Department;
using ElShaday.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Repositories;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    private readonly ElShadayContext _context;

    public DepartmentRepository(ElShadayContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> NameExistsAsync(string name)
        => await _context.Departments.AnyAsync(x =>
            x.Name.Equals(name)
            && !x.DeletedAt.HasValue
        );
}