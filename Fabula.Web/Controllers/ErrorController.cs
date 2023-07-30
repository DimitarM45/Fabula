namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class ErrorController : BaseController
{
    [AllowAnonymous]
    [HttpGet("/error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult HandleErrors(int statusCode)
    {
        switch(statusCode)
        {
            case 404:
                return View("Error404");

            case 401:
                return View("Error401");

            default:
                return View("Error");
        }
    }
}
