namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Composition;

using Microsoft.AspNetCore.Mvc;
using Fabula.Web.ViewModels.User;

public class UserController : BaseController
{
    private readonly IUserService userService;

    private readonly ICompositionService compositionService;

    public UserController(IUserService userService,
        ICompositionService compositionService)
    {
        this.userService = userService;
        this.compositionService = compositionService;
    }

    public async Task<IActionResult> MyWorks()
    {
        try
        {
            string? userId = userService.GetUserId();

            IEnumerable<CompositionProfileViewModel> compositionViewModels = await compositionService.GetAllForUserAsync(userId);

            return View(compositionViewModels);
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    public async Task<IActionResult> Profile(string userId)
    {
        try
        {
            UserProfileViewModel? profileViewModel = await userService.GetProfileAsync(userId);

            if (profileViewModel == null)
                return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

            return View(profileViewModel);
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }
}
