using ElShaday.Data.Context;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Repositories;

public class LegalPersonRepository : Repository<LegalPerson>, ILegalPersonRepository
{
    private readonly ElShadayContext _context;

    public LegalPersonRepository(ElShadayContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> DocumentExistsAsync(string document)
        => await _context.LegalPeople.AnyAsync(x =>
            x.Document.Value.Equals(document)
            && !x.DeletedAt.HasValue
        );

    public async Task<LegalPerson?> GetFullByIdAsync(int id)
        => await _context.LegalPeople
            .Include(x => x.Address)
            .Include(x => x.Departments.Where(y => !y.DeletedAt.HasValue))
            .FirstOrDefaultAsync(x =>
            x.Id == id
            && !x.DeletedAt.HasValue
        );
}