using ElShaday.Domain.Entities.Person;

namespace ElShaday.Domain.Interfaces;

public interface IPhysicalPersonRepository : IRepository<PhysicalPerson>
{
    Task<bool> DocumentExistsAsync(int? selfId, string document);
    Task<PhysicalPerson?> GetFullByIdAsync(int id);
    Task<IEnumerable<PhysicalPerson>> GetAvailableForDepartmentAsync();
}