using ElShaday.Data.Context;
using ElShaday.Domain.Entities;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly ElShadayContext _context;
    public Repository(ElShadayContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>()
            .AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.FromResult(_context.Set<T>().Update(entity));
        entity.Update();
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>()
        .AsNoTracking()
        .FirstOrDefaultAsync(x =>
            x.Id == id
            && !x.DeletedAt.HasValue
        );

    public async Task<Paged<T>> GetAsync(int page = 1, int pageSize = 25)
    {
        var result = await _context.Set<T>()
            .Where(x => !x.DeletedAt.HasValue)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        var total = await _context.Set<T>()
            .Where(x => !x.DeletedAt.HasValue)
            .CountAsync();

        var totalPages = (int)Math.Ceiling(total / (double)pageSize);

        return new Paged<T>(result, page, pageSize, total, totalPages);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if(entity is null)
            return;
        
        entity.Delete();
        await UpdateAsync(entity);
    }
}