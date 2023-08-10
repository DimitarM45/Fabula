namespace Fabula.Web.Areas.Admin.Controllers;

using Core.Contracts;
using ViewModels.Admin.Dashboard;

using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    private readonly IUserService userService;

    private readonly ICompositionService compositionService;

    public HomeController(IUserService userService, ICompositionService compositionService)
    {
        this.userService = userService;
        this.compositionService = compositionService;
    }

    public async Task<IActionResult> Dashboard()
    {
        DashboardViewModel dashboardViewModel = new DashboardViewModel();

        dashboardViewModel.Users = await userService.GetAllForAdminDashboardAsync();
        dashboardViewModel.Compositions = await compositionService.GetAllForAdminDashboardAsync();


        return View(dashboardViewModel);
    }
}
