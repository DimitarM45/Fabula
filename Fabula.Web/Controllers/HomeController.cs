namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class HomeController : BaseController
{
    [HttpGet]
    [AllowAnonymous]

    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet("/privacy")]

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("/area/select")]
    [Authorize(Roles = "Admin")]

    public IActionResult SelectArea()
    {
        return View();
    }
}