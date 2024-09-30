using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using CleanArchitecture.Application.Common;
//using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Domain.Entities;
public class Product 
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "وارد کردن نام محصول الزامی است")]
    [StringLength(100, ErrorMessage = "نام محصول نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
    public required string Name { get; set; }

    [StringLength(500, ErrorMessage = "توضیحات نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد")]
    public string? Description { get; set; }

    [Range(1_000,100_000_000 , ErrorMessage = "قیمت باید بین ۰.۰۱ تا ۱۰,۰۰۰.۰۰ باشد")]
    [DataType(DataType.Currency)]
    public required decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "تعداد باید ۰ یا بیشتر باشد")]
    public required int Quantity { get; set; }

    [Required(ErrorMessage = "کد گروه کالا الزامی می‌باشد")]
    [ForeignKey("ProductGroup")]
    public required int ProductGroupId { get; set; }

    // Navigation property for the ProductGroup relationship
    public virtual ProductGroup? ProductGroup { get; set; }
}
