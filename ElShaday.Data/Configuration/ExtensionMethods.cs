using System.Linq.Expressions;
using ElShaday.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElShaday.Data.Configuration;

public static class ExtensionMethods
{
    public static IQueryable<T> IncludeIf<T>(this IQueryable<T> query, bool condition, params Expression<Func<T, Entity>>[] includes) where T : class
    {
        if (!condition)
        {
            return query;
        }

        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }

        return query;
    }
    
    public static IQueryable<T> IncludeIf<T>(this IQueryable<T> query, bool condition, params string[] includes) where T : class
    {
        if (!condition)
        {
            return query;
        }

        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }

        return query;
    }
}