using CleanArchitecture.Infrastructure.Event;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers;
public class TestRabbit : Controller
{


    public IActionResult Morteza()
    {
        return Json(new { msg = "this project working fine" });
    }
}
