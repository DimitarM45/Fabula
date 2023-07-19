namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.Tag;
using Web.ViewModels.Genre;
using Web.ViewModels.Composition;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using System.Collections.Generic;

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
            .Where(c => c.DeletedOn == null)
            .Select(c => new CompositionAllViewModel()
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Synopsys = c.Synopsys,
                CoverUrl = c.CoverUrl,
                Author = c.Author.UserName,
                AuthorId = c.Author.Id.ToString(),
                HasAdultContent = c.hasAdultContent,
                Rating = !c.Ratings.Any() ? null : c.Ratings.Average(r => r.Value),
                PublishedOn = c.PublishedOn,
            })
            .ToListAsync();

        return compositionViewModels;
    }

    public async Task AddAsync(CompositionCreateFormModel formModel, string authorId)
    {
        Composition composition = new Composition()
        {
            Title = formModel.Title,
            Synopsys = formModel.Synopsys,
            Content = formModel.Content,
            CoverUrl = formModel.CoverUrl,
            AuthorId = Guid.Parse(authorId),
            PublishedOn = DateTime.Now,
            hasAdultContent = formModel.HasAdultContent,
        };

        foreach (int genreId in formModel.Genres)
        {
            Genre? genre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

            if (genre != null)
                composition.Genres.Add(genre);
        }

        if (composition.Genres.Count == 0)
            throw new InvalidOperationException("No valid genre was provided!");

        foreach (TagFormModel tagFormModel in formModel.Tags)
        {
            Tag? tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.Name == tagFormModel.Name);

            if (tag == null)
                tag = new Tag() { Name = tagFormModel.Name };

            composition.Tags.Add(tag);
        }

        await dbContext.Compositions.AddAsync(composition);

        await dbContext.SaveChangesAsync();
    }

    public Task<CompositionReadViewModel?> GetByIdAsync(string compositionId)
    {
        CompositionReadViewModel? compositionViewModel = await dbContext.Compositions
            .AsNoTracking()
            .Select(c => new CompositionReadViewModel()
            {
                Id = c.Id,
                Title = c.Id,
            })
            .FirstOrDefaultAsync(c => c.Id = compositionId);

        return compositionViewModel;
    }
}
