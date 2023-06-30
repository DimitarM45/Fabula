namespace Fabula.Web.Controllers;

using ViewModels.Home;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System.Diagnostics;

[Authorize]

public class HomeController : Controller
{
    [AllowAnonymous]

    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}