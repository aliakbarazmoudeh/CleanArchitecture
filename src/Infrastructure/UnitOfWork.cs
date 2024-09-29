using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        #region
        Products = new RepositoryService<Product>(context);
        ProductGroups = new RepositoryService<ProductGroup>(context);
        #endregion
    }
    #region Example Code
    public IRepositoryService<Product> Products { get; }
    public IRepositoryService<ProductGroup> ProductGroups { get; }
    #endregion
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
