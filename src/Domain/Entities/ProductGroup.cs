using System.ComponentModel.DataAnnotations;
//using ProductService.Application.Common.Interfaces;

namespace ProductService.Domain.Entities;
public class ProductGroup
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "وارد کردن نام گروه محصول الزامی است")]
    [StringLength(100, ErrorMessage = "نام گروه محصول نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
    public required string GroupName { get; set; }

    [StringLength(500, ErrorMessage = "توضیحات نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد")]
    public string? Description { get; set; }

    public virtual List<Product>? Products { get; set; }
}
