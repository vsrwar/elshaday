using ElShaday.Data.Context;
using ElShaday.Domain.Entities.User;
using ElShaday.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Repositories;

public class AdminUserRepository : Repository<AdminUser>, IAdminUserRepository
{
    private readonly ElShadayContext _context;
    public AdminUserRepository(ElShadayContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
        => await _context.AdminUsers.AnyAsync(x =>
            x.Email.Equals(email)
            && !x.DeletedAt.HasValue
        );

    public async Task<bool> NickNameExistsAsync(string nickName)
        => await _context.AdminUsers.AnyAsync(x =>
            x.NickName.Equals(nickName)
            && !x.DeletedAt.HasValue
        );

    public async Task<bool> IdExistsAsync(int id)
        => await _context.AdminUsers.AnyAsync(x =>
            x.Id == id
            && !x.DeletedAt.HasValue
        );
}