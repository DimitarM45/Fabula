namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.Tag;
using Web.ViewModels.Composition;
using static Common.GlobalConstants;

using Z.EntityFramework.Plus;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CompositionService : ICompositionService
{
    private readonly FabulaDbContext dbContext;

    public CompositionService(FabulaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<CompositionViewModel>> GetAllAsync()
    {
        IEnumerable<CompositionViewModel> compositionViewModels = await dbContext.Compositions
            .AsNoTracking()
            .Where(c => c.DeletedOn == null)
            .Select(c => new CompositionViewModel()
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Synopsis = c.Synopsis,
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

    public async Task<string> AddAsync(CompositionFormModel formModel, string authorId)
    {
        Composition composition = new Composition()
        {
            Title = formModel.Title,
            Synopsis = formModel.Synopsis,
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

        return composition.Id.ToString();
    }

    public async Task<CompositionReadViewModel?> GetByIdAsync(string compositionId)
    {
        Composition? composition = await dbContext.Compositions
            .AsNoTracking()
            .Include(c => c.Author)
            .Include(c => c.Comments)
            .ThenInclude(c => c.Author)
            .Include(c => c.Ratings)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(c => c.Id.ToString() == compositionId && c.DeletedOn == null);

        if (composition == null)
            return null;

        CompositionReadViewModel compositionReadViewModel = new CompositionReadViewModel()
        {
            Id = composition.Id.ToString(),
            Title = composition.Title,
            Synopsis = composition.Synopsis,
            Content = composition.Content,
            CoverUrl = composition.CoverUrl,
            Author = composition.Author.UserName,
            AuthorId = composition.Author.Id.ToString(),
            hasAdultContent = composition.hasAdultContent,
            PublishedOn = composition.PublishedOn,
            Favorites = composition.Favorites.Count()
        };

        return compositionReadViewModel;
    }

    public async Task<bool> DeleteByIdAsync(string compositionId)
    {
        Composition? composition = await dbContext.Compositions
            .FirstOrDefaultAsync(c => c.Id.ToString() == compositionId && c.DeletedOn == null);

        bool isSuccessful = false;

        if (composition != null)
        {
            composition.DeletedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();

            isSuccessful = true;
        }

        return isSuccessful;
    }

    public async Task<CompositionFormModel?> GetForEditAsync(string compositionId)
    {
        Composition? composition = await dbContext.Compositions
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id.ToString() == compositionId && c.DeletedOn == null);

        if (composition == null)
            return null;

        CompositionFormModel compositionFormModel = new CompositionFormModel()
        {
            Id = composition.Id.ToString(),
            Title = composition.Title,
            Synopsis = composition.Synopsis,
            Content = composition.Content,
            CoverUrl = composition.CoverUrl,
            AuthorId = composition.AuthorId.ToString(),
            HasAdultContent = composition.hasAdultContent
        };

        return compositionFormModel;
    }

    public async Task UpdateAsync(CompositionFormModel formModel)
    {
        Composition? compositionToUpdate = await dbContext.Compositions
            .FirstOrDefaultAsync(c => c.Id.ToString() == formModel.Id);

        if (compositionToUpdate != null)
        {
            ICollection<Genre> genresToUpdate = new List<Genre>();

            foreach (int genreId in formModel.Genres)
            {
                Genre? genre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

                if (genre != null)
                    genresToUpdate.Add(genre);

                // TODO: Figure out a way to decouple genres and tags (if added at all) from the composition service
            }

            if (genresToUpdate.Count == 0)
                throw new InvalidOperationException("No valid genre was provided!");

            compositionToUpdate.Title = formModel.Title;
            compositionToUpdate.Content = formModel.Content;
            compositionToUpdate.Synopsis = formModel.Synopsis;
            compositionToUpdate.CoverUrl = formModel.CoverUrl;
            compositionToUpdate.Genres = genresToUpdate;

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<string?> GetRandomIdAsync()
    {
        Random rng = new Random();

        int compositionCount = await dbContext.Compositions.CountAsync();

        int randomIndex = rng.Next(1, compositionCount);

        string? compositionId = await dbContext.Compositions
            .AsNoTracking()
            .OrderBy(c => Guid.NewGuid())
            .Skip(randomIndex)
            .Take(1)
            .Select(c => c.Id.ToString())
            .FirstOrDefaultAsync();

        return compositionId;
    }

    public async Task<bool> RestoreByIdAsync(string compositionId)
    {
        Composition? composition = await dbContext.Compositions
            .Where(c => c.DeletedOn != null)
            .FirstOrDefaultAsync(c => c.Id.ToString() == compositionId);

        bool isRestoredSuccessfully = false;

        if (composition != null && composition.DeletedOn >= DateTime.Now.AddDays(-CompositionRecoveryDayLimit))
        {
            composition.DeletedOn = null;

            isRestoredSuccessfully = true;

            await dbContext.SaveChangesAsync();
        }

        return isRestoredSuccessfully;
    }

    public async Task<IEnumerable<CompositionProfileViewModel>> GetAllForUserAsync(string userId)
    {
        throw new NotImplementedException(); 
    }
}
