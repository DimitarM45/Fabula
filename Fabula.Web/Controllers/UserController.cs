namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.User;
using ViewModels.Composition;

using Microsoft.AspNetCore.Mvc;

public class UserController : BaseController
{
    private readonly IUserService userService;

    private readonly ICompositionService compositionService;

    private readonly IGenreService genreService;

    public UserController(IUserService userService,
        ICompositionService compositionService,
        IGenreService genreService)
    {
        this.userService = userService;
        this.compositionService = compositionService;
        this.genreService = genreService;
    }

    public async Task<IActionResult> Profile(string userId)
    {
        try
        {
            UserProfileViewModel? profileViewModel = await userService.GetProfileAsync(userId);

            if (profileViewModel == null)
                return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

            profileViewModel.FavoriteGenres = await genreService.GetForUserAsync(userId);

            if (profileViewModel == null)
                return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

            return View(profileViewModel);
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    public async Task<IActionResult> Works(string userId)
    {
        try
        {
            IEnumerable<CompositionProfileViewModel> compositionViewModels = await compositionService.GetAllForUserAsync(userId);

            return View("All", new { compositionViewModels });
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    public async Task<IActionResult> DeletedWorks()
    {
        return View();
    }
}
