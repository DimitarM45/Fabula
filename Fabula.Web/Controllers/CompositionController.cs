﻿namespace Fabula.Web.Controllers;

using Core.Contracts;
using Web.ViewModels.Composition;
using Web.Infrastructure.ModelBinders;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

public class CompositionController : BaseController
{
    private readonly IGenreService genreService;

    private readonly ICompositionService compositionService;

    public CompositionController(IGenreService genreService,
        ICompositionService compositionService)
    {
        this.genreService = genreService;
        this.compositionService = compositionService;
    }

    [AllowAnonymous]

    public async Task<IActionResult> All()
    {
        IEnumerable<CompositionAllViewModel> compositionViewModels = await compositionService.GetAllAsync();

        return View(compositionViewModels);
    }

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        CompositionCreateFormModel compositionCreateFormModel = new CompositionCreateFormModel();

        compositionCreateFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();

        return View(compositionCreateFormModel);
    }

    [HttpPost]

    public async Task<IActionResult> Create([ModelBinder(typeof(GenreListModelBinder))]
        CompositionCreateFormModel formModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Invalid input when creating composition!");

            formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

            return View(formModel);
        }

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await compositionService.AddAsync(formModel, userId);

        return RedirectToAction("Details", "Composition");
    }

    [HttpGet]

    public async Task<IActionResult> Details(string compositionId)
    {
        

        return View();
    }
}
