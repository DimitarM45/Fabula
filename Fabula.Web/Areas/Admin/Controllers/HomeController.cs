namespace Fabula.Web.Areas.Admin.Controllers;

using Core.Contracts;
using Infrastructure.Utilities; 
using ViewModels.Admin.Dashboard;

using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;

public class HomeController : BaseController
{
    private readonly IUserService userService;

    private readonly ICompositionService compositionService;

    private readonly ILogger logger;

    public HomeController(IUserService userService, ICompositionService compositionService, ILogger<HomeController> logger)
    {
        this.userService = userService;
        this.compositionService = compositionService;
        this.logger = logger;
    }

    public async Task<IActionResult> Dashboard()
    {
        DashboardViewModel dashboardViewModel = new DashboardViewModel();

        try
        {
            dashboardViewModel.Users = await userService.GetAllForAdminDashboardAsync();
            dashboardViewModel.Compositions = await compositionService.GetAllForAdminDashboardAsync();
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        return View(dashboardViewModel);
    }
}
