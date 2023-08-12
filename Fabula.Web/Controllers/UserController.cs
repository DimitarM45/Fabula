namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.User;
using Infrastructure.Utilities;

using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Genre;
using static Common.Messages.ErrorMessages.Shared;
using static Common.Messages.SuccessMessages.Genre;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet]
    [AllowAnonymous]

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

    [HttpPost]

    public async Task<IActionResult> FavoriteGenre(int genreId)
    {
        string? userId = userService.GetUserId();

        try
        {
            if (userId != null)
            {
                await userService.AddFavoriteGenreToUserAsync(userId, genreId);

                TempData[SuccessNotification] = SuccessfulFavoriteGenreAddMessage;

                return RedirectToAction("Profile", new { UserId = userId });
            }

            TempData[ErrorNotification] = FailedGenreFavoriteAddition;

            return RedirectToAction("All", "Genre");
        }
        catch (Exception e)
        {
            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> UnfavoriteGenre(int genreId)
    {
        string? userId = userService.GetUserId();

        try
        {
            if (userId != null)
            {
                await userService.RemoveFavoriteGenreFromUserAsync(userId, genreId);

                TempData[SuccessNotification] = SuccessfulFavoriteGenreRemoveMessage;

                return RedirectToAction("Profile", new { UserId = userId });
            }

            TempData[ErrorNotification] = FailedGenreFavoriteRemoval;

            return RedirectToAction("Profile", new { UserId = userId });
        }
        catch (Exception e)
        {
            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }
}
