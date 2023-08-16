using ElShaday.Domain.Entities.User;

namespace ElShaday.Domain.Interfaces;

public interface IAdminUserRepository : IRepository<AdminUser>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NickNameExistsAsync(string nickName);
    Task<bool> IdExistsAsync(int id);
}