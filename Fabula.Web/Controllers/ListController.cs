namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

public class ListController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
