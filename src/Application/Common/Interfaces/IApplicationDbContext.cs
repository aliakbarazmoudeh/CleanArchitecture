using ProductService.Application.Common.Models;
using ProductService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ProductService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    DbSet<ProductGroup> ProductGroups { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
