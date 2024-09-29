using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public virtual async Task DeleteByIdAsync(int id) {

        var entity = await GetEntityByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
    #endregion
}
