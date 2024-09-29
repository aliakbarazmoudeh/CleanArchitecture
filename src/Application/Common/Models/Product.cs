using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Common.Models;
public class Product : IProduct
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "وارد کردن نام محصول الزامی است")]
    [StringLength(100, ErrorMessage = "نام محصول نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
    public required string Name { get; set; }

    [StringLength(500, ErrorMessage = "توضیحات نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد")]
    public string? Description { get; set; }

    [Range(0.01, 10000.00, ErrorMessage = "قیمت باید بین ۰.۰۱ تا ۱۰,۰۰۰.۰۰ باشد")]
    [DataType(DataType.Currency)]
    public required decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "تعداد باید ۰ یا بیشتر باشد")]
    public required int Quantity { get; set; }

    [ForeignKey("ProductGroup")]
    public required int ProductGroupId { get; set; }

    // Navigation property for the ProductGroup relationship
    public virtual required ProductGroup ProductGroup { get; set; }
}
