using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.Common.Models;
using ProductService.Domain.Entities;

namespace ProductService.Application.Common.Interfaces;
public interface IUnitOfWork
{
    // Sample Codes
    IRepositoryService<Product> Products { get; }
    IRepositoryService<ProductGroup> ProductGroups { get; }
    Task<int> SaveAsync();
    public void Dispose();
}
