namespace Fabula.Web.ViewComponents;

using Core.Contracts;
using ViewModels.Rating;

using Microsoft.AspNetCore.Mvc;

public class RatingsPreviewViewComponent : ViewComponent
{
    private readonly IRatingService ratingService;

    public RatingsPreviewViewComponent(IRatingService ratingService)
    {
        this.ratingService = ratingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int numberOfRatings, string compositionId)
    {
        IEnumerable<RatingViewModel> ratingViewModels = await ratingService.GetRatingsByIdAsync(compositionId);

        return View("_RatingsPreviewPartial", ratingViewModels.Take(numberOfRatings));
    }
}
