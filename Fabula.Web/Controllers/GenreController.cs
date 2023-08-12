namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Genre;
using Infrastructure.Utilities;

using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

public class GenreController : BaseController
{
    private readonly IGenreService genreService;

    private readonly ILogger logger;

    public GenreController(IGenreService genreService, ILogger<GenreController> logger)
    {
        this.genreService = genreService;
        this.logger = logger;
    }

    [AllowAnonymous]

    public async Task<IActionResult> All()
    {
        try
        {
            IEnumerable<AllGenreViewModel> genreViewModels = await genreService.GetAllAsync();

            return View(genreViewModels);
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }
}
