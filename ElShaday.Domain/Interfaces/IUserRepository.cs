using ElShaday.Domain.Entities.User;

namespace ElShaday.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NickNameExistsAsync(string nickName);
    Task<bool> IdExistsAsync(int id);
}