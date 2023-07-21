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
using Fabula.Web.ViewModels.User;

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
            .Include(c => c.Ratings)
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
                Author = new UserViewModel()
                {
                    Id = c.AuthorId.ToString(),
                    Username = c.Author.UserName,
                    ProfilePictureUrl = c.Author.ProfilePictureUrl
                },
                CompositionId = c.Composition.Id.ToString(),
                PublishedOn = c.PublishedOn,
                Likes = c.Likes.Count()
            })
            .ToList(),
            Favorites = composition.Favorites.Count(),
            Ratings = composition.Ratings.Select(r => new RatingViewModel()
            {
                Id = r.Id.ToString(),
                Value = r.Value,
                Author = new UserViewModel()
                {
                    Id = r.Id.ToString(),
                    Username = r.User.UserName,
                    ProfilePictureUrl = r.User.ProfilePictureUrl
                },
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
            HasAdultContent = composition.hasAdultContent,
            Genres = composition.Genres.Select(g => g.Id),

            //TODO: Add tags if necessary
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
            }

            if (genresToUpdate.Count == 0)
                throw new InvalidOperationException("No valid genre was provided!");

            compositionToUpdate.Title = formModel.Title;
            compositionToUpdate.Content = formModel.Content;
            compositionToUpdate.Synopsis = formModel.Synopsis;
            compositionToUpdate.CoverUrl = formModel.CoverUrl;
            compositionToUpdate.Genres = genresToUpdate;
        }

        await dbContext.SaveChangesAsync();
    }
}
