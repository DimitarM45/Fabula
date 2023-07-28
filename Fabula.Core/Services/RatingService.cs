namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.Rating;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

public class RatingService : IRatingService
{
    private readonly FabulaDbContext dbContext;

    public RatingService(FabulaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<RatingViewModel>> GetRatingsByIdAsync(string compositionId)
    {
        IEnumerable<RatingViewModel> ratingViewModels = await dbContext.Ratings
            .AsNoTracking()
            .Select(r => new RatingViewModel()
            {
                Id = r.Id.ToString(),
                Value = r.Value,
                PublishedOn = r.PublishedOn
            })
            .ToListAsync();

        return ratingViewModels;
    }

    public async Task<bool> DeleteByIdAsync(string ratingId)
    {
        Rating? rating = await dbContext.Ratings
            .FirstOrDefaultAsync(r => r.Id.ToString() == ratingId && r.DeletedOn == null);

        bool isSuccessful = false;

        if (rating != null)
        {
            rating.DeletedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();

            isSuccessful = true;
        }

        return isSuccessful;
    }
}
