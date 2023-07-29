namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Composition;

using Microsoft.AspNetCore.Mvc;

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
        string? userId = userService.GetUserId();

        IEnumerable<CompositionProfileViewModel> compositionViewModels = await compositionService.GetAllForUserAsync(userId);

        return View(compositionViewModels);
    }

    public async Task<IActionResult> Profile(string userId)
    {


        return View();
    }
}
