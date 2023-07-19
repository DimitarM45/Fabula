namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.Tag;
using Web.ViewModels.Genre;
using Web.ViewModels.Rating;
using Web.ViewModels.Comment;
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

    public async Task<CompositionReadViewModel?> GetByIdAsync(string compositionId)
    {
        Composition? composition = await dbContext.Compositions
            .AsNoTracking()
            .Include(c => c.Author)
            .FirstOrDefaultAsync(c => c.Id.ToString() == compositionId && c.DeletedOn == null);

        if (composition == null)
            return null;

        CompositionReadViewModel compositionReadViewModel = new CompositionReadViewModel()
        {
            Id = composition.Id.ToString(),
            Title = composition.Title,
            Synopsys = composition.Synopsys,
            Content = composition.Content,
            CoverUrl = composition.CoverUrl,
            Author = composition.Author.UserName,
            AuthorId = composition.Author.Id.ToString(),
            hasAdultContent = composition.hasAdultContent,
            PublishedOn = composition.PublishedOn,
            Genres = composition.Genres.Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToList(),
            Comments = composition.Comments.Select(c => new CommentViewModel()
            {
                Id = c.Id.ToString(),
                Content = c.Id.ToString(),
                Author = c.Author.UserName,
                AuthorId = c.Author.Id.ToString(),
                CompositionId = c.Composition.Id,
                PublishedOn = c.PublishedOn,
                Likes = c.Likes.Count()
            })
            .ToList(),
            Favorites = composition.Favorites.Count(),
            Ratings = composition.Ratings.Select(r => new RatingViewModel()
            {
                Id = r.Id.ToString(),
                Value = r.Value,
                User = r.User.UserName,
                UserId = r.User.Id.ToString(),
                PublishedOn = r.PublishedOn
            })

            //TODO: Add tags if necessary
        };

        return compositionReadViewModel;
    }

    public async Task DeleteById(string compositionId)
    {
        Composition? composition = await dbContext.Compositions
            .FirstOrDefaultAsync(c => c.Id.ToString() == compositionId && c.DeletedOn == null);

        if (composition != null)
            composition.DeletedOn = DateTime.Now;

        await dbContext.SaveChangesAsync();
    }
}
