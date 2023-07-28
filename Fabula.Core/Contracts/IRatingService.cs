namespace Fabula.Core.Contracts;

using Web.ViewModels.Rating;

public interface IRatingService
{
    Task<IEnumerable<RatingViewModel>> GetRatingsByIdAsync(string compositionId);

    Task<bool> DeleteByIdAsync(string ratingId);
}
