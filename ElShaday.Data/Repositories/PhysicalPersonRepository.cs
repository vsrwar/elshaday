using ElShaday.Data.Context;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Repositories;

public class PhysicalPersonRepository : Repository<PhysicalPerson>, IPhysicalPersonRepository
{
    private readonly ElShadayContext _context;

    public PhysicalPersonRepository(ElShadayContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<bool> DocumentExistsAsync(string document)
        => await _context.PhysicalPeople.AnyAsync(x =>
            x.Document.Value.Equals(document)
            && !x.DeletedAt.HasValue
        );

    public async Task<PhysicalPerson?> GetFullByIdAsync(int id)
        => await _context.PhysicalPeople
            .Include(x => x.Address)
            .Include(x => x.Departments.Where(y => !y.DeletedAt.HasValue))
            .FirstOrDefaultAsync(x =>
                x.Id == id
                && !x.DeletedAt.HasValue
            );
}