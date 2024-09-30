using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Common.Interfaces;
public interface IProductGroup
{
    int Id { get; set; }
    string GroupName { get; set; }
    string? Description { get; set; }
}
