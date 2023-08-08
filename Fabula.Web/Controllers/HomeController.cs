namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Home;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class HomeController : BaseController
{
    private readonly IUserService userService;

    private readonly ICompositionService compositionService;

    public HomeController(IUserService userService, ICompositionService compositionService)
    {
        this.userService = userService;
        this.compositionService = compositionService;
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel();

        try
        {
            homeViewModel.CompositionsCount = await compositionService.GetCountAsync();
            homeViewModel.UsersCount = await userService.GetCountAsync();
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        return View(homeViewModel);
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