﻿using ElShaday.Data.Context;
using ElShaday.Domain.Entities.User;
using ElShaday.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ElShadayContext _context;
    public UserRepository(ElShadayContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
        => await _context.Users.AnyAsync(x =>
            x.Email.Equals(email)
            && !x.DeletedAt.HasValue
        );

    public async Task<bool> NickNameExistsAsync(int? selfId, string nickName)
    {
        if(selfId.HasValue)
            return await _context.Users.AnyAsync(x =>
                x.NickName.Equals(nickName)
                && !x.DeletedAt.HasValue
                && x.Id != selfId.Value
            );
        
        return await _context.Users.AnyAsync(x =>
            x.NickName.Equals(nickName)
            && !x.DeletedAt.HasValue
        ); 
    }

    public async Task<User?> GetByNickNameAsync(string nick)
        => await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.NickName == nick
                && !x.DeletedAt.HasValue
            ); 
}