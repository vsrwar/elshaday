using ElShaday.Domain.Entities.Person;

namespace ElShaday.Domain.Interfaces;

public interface ILegalPersonRepository : IRepository<LegalPerson>
{
    Task<bool> DocumentExistsAsync(string document);
    Task<LegalPerson?> GetFullByIdAsync(int id);
}