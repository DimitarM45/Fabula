namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]

public class ListController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
