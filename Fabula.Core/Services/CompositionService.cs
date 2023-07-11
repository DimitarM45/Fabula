namespace Fabula.Core.Services;

using Data;
using Contracts;
using Web.ViewModels.Composition;

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class CompositionService : ICompositionService
{
    private readonly FabulaDbContext dbContext;

    public CompositionService(FabulaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<CompositionAllViewModel>> GetAllAsync()
    {
        IEnumerable<CompositionAllViewModel> compositionViewModels = await dbContext.Compositions
            .AsNoTracking()
            .Where(c => c.DeletedOn != null)
            .Select(c => new CompositionAllViewModel()
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Synopsys = c.Synopsys,
                CoverUrl = c.CoverUrl,
                Author = c.Author.UserName,
                AuthorId = c.Author.Id.ToString(),
                HasAdultContent = c.hasAdultContent,
                Rating = c.Ratings.Average(r => r.Value),
                PublishedOn = c.PublishedOn,
            })
            .ToListAsync();

        return compositionViewModels;
    }
}
