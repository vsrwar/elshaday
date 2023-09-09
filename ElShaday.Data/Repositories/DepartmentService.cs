using ElShaday.Data.Context;
using ElShaday.Domain.Entities.Department;
using ElShaday.Domain.Entities.Person;
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

    public async Task<bool> HasDepartmentsAsync(int personId, PersonType personType)
    {
        return personType switch
        {
            PersonType.Legal => await _context.Departments.AnyAsync(x =>
                x.LegalPersonId == personId
                && !x.DeletedAt.HasValue
            ),
            PersonType.Physical => await _context.Departments.AnyAsync(x =>
                x.PhysicalPersonId == personId
                && !x.DeletedAt.HasValue
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(personType), personType, null)
        };
    }
}