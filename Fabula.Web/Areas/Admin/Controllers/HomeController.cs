namespace Fabula.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    public async Task<IActionResult> Dashboard()
    {
        return View();
    }
}
