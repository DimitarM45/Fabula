namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Home;
using Infrastructure.Utilities;

using static Common.GlobalConstants;
using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

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
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        return View(homeViewModel);
    }

    [AllowAnonymous]
    [HttpGet("/privacy")]

    public IActionResult Privacy()
        => View();

    [HttpGet("/area/select")]
    [Authorize(Roles = AdminRoleName)]

    public IActionResult SelectArea()
        => View();
}