using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Interfaces;
namespace CleanArchitecture.Web.Controllers.Products;
public class ProductsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _unitOfWork.Products.GetAllAsync() ; 
        return Json(products);
    }
}
