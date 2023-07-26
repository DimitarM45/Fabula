namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class HomeController : BaseController
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

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult Error(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                return View("Error404");

            case 401:
                return View("Error401");
        }

        return View();
    }
}