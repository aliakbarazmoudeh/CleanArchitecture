using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Infrastructure;
public class RepositoryService<T> : IRepositoryService<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public RepositoryService(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    #region Get Entity From the Database Functions
    public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public virtual async Task<T?> GetEntityByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<T>> GetByConditionAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking is true)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }
    #endregion 

    #region Add Entity To Database Functions
    public virtual async Task AddEntityAsync(T entity) => await _dbSet.AddAsync(entity);
    #endregion

    #region Updata Entity From Database Functions
    public virtual void UpdateEntityAsync(T entity) => _dbSet.Update(entity);
    #endregion

    #region Delete Entity From Databae Functions
    public virtual void DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        
    }

    public virtual async Task<bool> DeleteByIdAsync(int id) {

        var entity = await GetEntityByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            return true;
        }
        return false;
    }
    #endregion
}
