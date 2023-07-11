namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Genre;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class GenreController : BaseController
{
    private readonly IGenreService genreService;

    public GenreController(IGenreService genreService)
    {
        this.genreService = genreService;
    }

    [AllowAnonymous]

    public async Task<IActionResult> All()
    {
        IEnumerable<AllGenreViewModel> genreViewModels = await genreService.GetAllAsync();

        return View(genreViewModels);
    }
}
