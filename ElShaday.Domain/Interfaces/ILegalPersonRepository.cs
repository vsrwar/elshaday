using ElShaday.Domain.Entities.Person;

namespace ElShaday.Domain.Interfaces;

public interface ILegalPersonRepository : IRepository<LegalPerson>
{
    Task<bool> DocumentExistsAsync(int? selfId, string document);
    Task<LegalPerson?> GetFullByIdAsync(int id);
    Task<IEnumerable<LegalPerson>> GetAvailableForDepartmentAsync();
}