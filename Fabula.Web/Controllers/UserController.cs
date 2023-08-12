namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.User;
using Infrastructure.Utilities;

using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;

public class UserController : BaseController
{
    private readonly IUserService userService;

    private readonly ICompositionService compositionService;

    private readonly IGenreService genreService;

    private readonly ILogger logger;

    public UserController(
        IUserService userService,
        ICompositionService compositionService,
        IGenreService genreService,
        ILogger<UserController> logger)
    {
        this.userService = userService;
        this.compositionService = compositionService;
        this.genreService = genreService;
        this.logger = logger;
    }

    public async Task<IActionResult> Profile(string userId)
    {
        try
        {
            UserProfileViewModel? profileViewModel = await userService.GetProfileAsync(userId);

            if (profileViewModel == null)
            {
                TempData[WarningNotification] = 
                    string.Format(ResourceNotFoundErrorMessage, GetControllerName().ToLower());

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 404 });
            }

            profileViewModel.FavoriteGenres = await genreService.GetForUserAsync(userId);

            return View(profileViewModel);
        }
        catch (Exception e)
        {
            string? user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    public async Task<IActionResult> DeletedWorks()
    {
        return View();
    }
}
