using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces;
public interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
    string? Description { get; set; }
    decimal Price { get; set; }
    int Quantity { get; set; }
    int ProductGroupId { get; set; }

}
