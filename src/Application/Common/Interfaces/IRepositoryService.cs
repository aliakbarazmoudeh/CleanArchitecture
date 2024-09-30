using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Application.Common.Interfaces;
public interface IRepositoryService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetEntityByIdAsync(int id);
    Task<IEnumerable<T>> GetByConditionAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool disableTracking = true);
    Task AddEntityAsync(T entity);
    void UpdateEntityAsync(T entity);
    void DeleteAsync(T entity);
    Task<bool> DeleteByIdAsync(int id);
}
