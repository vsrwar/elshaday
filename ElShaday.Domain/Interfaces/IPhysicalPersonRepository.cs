using ElShaday.Domain.Entities.Person;

namespace ElShaday.Domain.Interfaces;

public interface IPhysicalPersonRepository : IRepository<PhysicalPerson>
{
    Task<bool> DocumentExistsAsync(string document);
    Task<PhysicalPerson?> GetFullByIdAsync(int id);
}