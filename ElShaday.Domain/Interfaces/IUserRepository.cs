using ElShaday.Domain.Entities.User;

namespace ElShaday.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NickNameExistsAsync(int? selfId, string nickName);
    Task<User?> GetByNickNameAsync(string nick);
}