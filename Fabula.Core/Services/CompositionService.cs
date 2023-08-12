namespace Fabula.Core.Services;

using Data;
using Enums;
using Contracts;
using Data.Models;
using ServiceModels;
using Web.ViewModels.User;
using Web.ViewModels.Genre;
using Web.ViewModels.Composition;
using Web.ViewModels.Admin.Composition;

using static Common.GlobalConstants;
using static Common.Messages.ErrorMessages.Genre;

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

    public async Task<CompositionQueryModel> GetAllAsync(IEnumerable<int>? selectedGenres = null,
        string? userId = null,
        string? searchTerm = null,
        int currentPage = 1,
        int compositionsPerPage = 1,
        DateSorting dateSorting = DateSorting.Newest,
        RatingSorting ratingSorting = RatingSorting.BestRated)
    {
        IQueryable<Composition>? compositionsQuery = null;

        if (!string.IsNullOrWhiteSpace(userId) && userId != string.Empty)
        {
            compositionsQuery = dbContext.Compositions
                .AsNoTracking()
                .Where(c => c.AuthorId.ToString() == userId && c.DeletedOn == null)
                .AsQueryable();
        }
        else
        {
            compositionsQuery = dbContext.Compositions
                .AsNoTracking()
                .Where(c => c.DeletedOn == null)
                .AsQueryable();
        }

        if (selectedGenres != null && selectedGenres.Any())
        {
            compositionsQuery = dbContext.Compositions
                .AsNoTracking()
                .Where(c => c.Genres.Any(g => selectedGenres.Contains(g.GenreId)));
        }

        if (!string.IsNullOrWhiteSpace(searchTerm) && searchTerm != string.Empty)
        {
            compositionsQuery = compositionsQuery
                .Where(c =>
                    c.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Synopsis.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Content.ToLower().Contains(searchTerm.ToLower()));
        }

        if (dateSorting == DateSorting.Oldest)
        {
            if (ratingSorting == RatingSorting.WorstRated)
            {
                compositionsQuery = compositionsQuery
                    .OrderBy(c => c.PublishedOn)
                    .ThenBy(c => c.Ratings.Average(r => r.Value));
            }
            else if (ratingSorting == RatingSorting.BestRated)
            {
                compositionsQuery = compositionsQuery
                    .OrderBy(c => c.PublishedOn)
                    .ThenByDescending(c => c.Ratings.Average(r => r.Value));
            }
        }
        else if (dateSorting == DateSorting.Newest)
        {
            if (ratingSorting == RatingSorting.WorstRated)
            {
                compositionsQuery = compositionsQuery
                    .OrderByDescending(c => c.PublishedOn)
                    .ThenBy(c => c.Ratings.Average(r => r.Value));
            }
            else if (ratingSorting == RatingSorting.BestRated)
            {
                compositionsQuery = compositionsQuery
                    .OrderByDescending(c => c.PublishedOn)
                    .ThenByDescending(c => c.Ratings.Average(r => r.Value));
            }
        }

        IEnumerable<CompositionViewModel> compositionViewModels = await compositionsQuery
            .Skip((currentPage - 1) * compositionsPerPage)
            .Take(compositionsPerPage)
            .Select(c => new CompositionViewModel()
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Synopsis = c.Synopsis,
                CoverUrl = c.CoverUrl,
                Author = new UserViewModel()
                {
                    Id = c.AuthorId.ToString(),
                    Username = c.Author.UserName,
                    ProfilePictureUrl = c.Author.ProfilePictureUrl
                },
                HasAdultContent = c.hasAdultContent,
                Rating = !c.Ratings.Any() ? null : c.Ratings.Average(r => r.Value),
                PublishedOn = c.PublishedOn,
                Genres = c.Genres.Select(g => new GenreViewModel()
                {
                    Id = g.Genre.Id,
                    Name = g.Genre.Name
                })
                .ToList()
            })
            .ToListAsync();

        CompositionQueryModel compositionsQueryModel = new CompositionQueryModel()
        {
            Compositions = compositionViewModels,
            CompositionsCount = compositionsQuery.Count(),
            UserId = userId
        };

        return compositionsQueryModel;
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
            CompositionGenre? genre = await dbContext.CompositionsGenres.FirstOrDefaultAsync(g => g.GenreId == genreId);

            if (genre != null)
                composition.Genres.Add(genre);
        }

        if (composition.Genres.Count == 0)
            throw new InvalidOperationException(NoGenreErrorMessage);

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
            Author = new UserViewModel()
            {
                Id = composition.AuthorId.ToString(),
                Username = composition.Author.UserName,
                ProfilePictureUrl = composition.Author.ProfilePictureUrl
            },
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
            .FirstOrDefaultAsync(c => c.Id.ToString() == formModel.Id && c.DeletedOn == null);

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
                throw new InvalidOperationException(NoGenreErrorMessage);

            compositionToUpdate.Title = formModel.Title;
            compositionToUpdate.Content = formModel.Content;
            compositionToUpdate.Synopsis = formModel.Synopsis;
            compositionToUpdate.CoverUrl = formModel.CoverUrl;

            // TODO: FIX

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<string?> GetRandomIdAsync()
    {
        int compositionCount = await dbContext.Compositions.CountAsync();

        if (compositionCount == 0)
            return null;

        string? compositionId = await dbContext.Compositions
            .AsNoTracking()
            .Where(c => c.DeletedOn == null)
            .OrderBy(c => Guid.NewGuid())
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

    public async Task<int> GetCountAsync()
    {
        int count = await dbContext.Compositions.CountAsync(c => c.DeletedOn == null);

        return count;
    }

    public async Task<IEnumerable<CompositionDashboardViewModel>> GetAllForAdminDashboardAsync()
    {
        IEnumerable<CompositionDashboardViewModel> compositionDashboardViewModels = await dbContext.Compositions
            .AsNoTracking()
            .Select(c => new CompositionDashboardViewModel()
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Author = new UserViewModel()
                {
                    Id = c.AuthorId.ToString(),
                    Username = c.Author.UserName,
                    ProfilePictureUrl = c.Author.ProfilePictureUrl
                },
                DeletedOn = c.DeletedOn,
                PublishedOn = c.PublishedOn,
                Comments = c.Comments.Count(c => c.DeletedOn == null),
                Ratings = c.Ratings.Count(c => c.DeletedOn == null),
                Rating = c.Ratings.Count(c => c.DeletedOn == null) == 0 ? 0.0 : c.Ratings.Average(r => r.Value)
            })
            .ToListAsync();

        return compositionDashboardViewModels;
    }
}
