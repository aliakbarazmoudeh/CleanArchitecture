using ProductService.Application.Common.Interfaces;
using ProductService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Web.Controllers.Products;

[Route("Categories")]
public class ProductGroupsController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGroupsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("List")]
    public async Task<IActionResult> Index()
    {
        List<ProductGroup> products = (await _unitOfWork.ProductGroups.GetAllAsync()).ToList();
        return Json(products.Select(p => new
        {
            p.Id,
            p.GroupName,
            p.Description,
        }
        ));
    }

    [HttpGet("Create")]
    public async Task<IActionResult> CreateProductGroup([FromQuery] ProductGroup entity)
    {
        var validationResult = ValidateModelState();
        if(validationResult is not null)
            return validationResult;

        await _unitOfWork.ProductGroups.AddEntityAsync(entity);
        await _unitOfWork.SaveAsync();  
        return Json(new { success = true, message = "this is work fine too" });
    }

    [HttpGet("Update")]
    public async Task<IActionResult> UpdateProductGroup([FromQuery] int productGroupId)
    {

        var product = await _unitOfWork.ProductGroups.GetEntityByIdAsync(productGroupId);
        return Json(new { success = true, message = "updating product group", product });
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteProductGroup(int productGroupId)
    {

        var product = await _unitOfWork.ProductGroups.GetEntityByIdAsync(productGroupId);
        return Json(new { success = true, message = "this is work fine too", product });
    }

}
