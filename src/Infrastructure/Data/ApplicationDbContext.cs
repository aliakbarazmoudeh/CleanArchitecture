using System.Reflection;
using ProductService.Application.Common.Interfaces;
using ProductService.Application.Common.Models;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser> , IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
