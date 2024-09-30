using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Common.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Web.Controllers.Products;


[Route("Products")]
public class ProductsController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return Json(products);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> CreateProductAsync([FromQuery] Product entity)
    {
        var validationResult = ValidateModelState();
            
        if(validationResult is not null)
            return validationResult;


        await _unitOfWork.Products.AddEntityAsync(entity);
        await _unitOfWork.SaveAsync();
        return Json(new { success = true, entity });
    }

    [HttpGet("Update")]
    public async Task<IActionResult> UpdateProductAsync([FromQuery] int id)
    {

        Product? product = (await _unitOfWork.Products.GetByConditionAsync(p => p.Id == id, null, null)).FirstOrDefault();

        if (product is null)
        {

            return Json(new { success = false, message = "can't find the product" });
        }

        return Json(new { success = true, message = "updated", product });
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteProductAsync([FromQuery] int id)
    {

        bool successfullyDeleted = await _unitOfWork.Products.DeleteByIdAsync(id);
        if (successfullyDeleted)
        {
            return Json(new { success = true, message = "با موفقیت محصول مورد نظر حذف گردید" });
        }
        return Json(new { success = false, message = "مشکلی در حین اجرای دستور شما پیش امده مجددا تلاش کنید" });
    }
}
