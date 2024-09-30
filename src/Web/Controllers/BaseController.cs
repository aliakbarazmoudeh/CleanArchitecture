using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers;
public class BaseController : Controller
{
    protected IActionResult? ValidateModelState()
    {
        if (ModelState.IsValid is not true)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return Json(new { success = false, message = "Validation failed", errors });
        }
        return null;
    }
}
