namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.Genre;

using static Common.Messages.ErrorMessages.Genre;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using System.Collections.Generic;

public class GenreService : IGenreService
{
    private readonly FabulaDbContext dbContext;

    public GenreService(FabulaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<AllGenreViewModel>> GetAllAsync()
    {
        IEnumerable<AllGenreViewModel> viewModels = await dbContext.Genres
            .AsNoTracking()
            .Select(g => new AllGenreViewModel()
            {
                Id = g.Id,
                Name = g.Name,
                CompositionCount = g.Compositions.Count(c => c.Composition.DeletedOn == null)
            })
            .ToListAsync();

        return viewModels;
    }

    public async Task<IEnumerable<GenreViewModel>> GetAllForSelectAsync()
    {
        IEnumerable<GenreViewModel> genreViewModels = await dbContext.Genres
            .AsNoTracking()
            .Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToListAsync();

        return genreViewModels;
    }

    public async Task<IEnumerable<GenreViewModel>> GetByIdAsync(string compositionId)
    {
        IEnumerable<GenreViewModel> genreViewModels = await dbContext.Compositions
            .AsNoTracking()
            .Where(c => c.Id.ToString() == compositionId && c.DeletedOn == null)
            .SelectMany(c => c.Genres
                .Select(g => new GenreViewModel()
                {
                    Id = g.GenreId,
                    Name = g.Genre.Name,
                }))
            .ToListAsync();

        return genreViewModels;
    }

    public async Task<IEnumerable<GenreViewModel>> GetForUserAsync(string userId)
    {
        IEnumerable<GenreViewModel> genreViewModels = await dbContext.Genres
            .AsNoTracking()
            .Where(g => g.Favorites.Any(f => f.UserId.ToString() == userId))
            .Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToListAsync();

        return genreViewModels;
    }

    public async Task AddToCompositionAsync(string compositionId, IEnumerable<int> genreIds)
    {
        ICollection<CompositionGenre> genresToAdd = new List<CompositionGenre>();

        foreach (int genreId in genreIds)
        {
            Genre? genre = await dbContext.Genres
                .FirstOrDefaultAsync(g => g.Id == genreId);

            if (genre == null)
                continue;

            CompositionGenre genreToAdd = new CompositionGenre()
            {
                CompositionId = Guid.Parse(compositionId),
                GenreId = genreId
            };

            genresToAdd.Add(genreToAdd);
        }

        if (genresToAdd.Count == 0)
            throw new InvalidOperationException(NoGenreErrorMessage);

        await dbContext.CompositionsGenres.AddRangeAsync(genresToAdd);

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveFromCompositionAsync(string compositionId, IEnumerable<int> genreIds)
    {
        IEnumerable<CompositionGenre> genresToDelete = await dbContext.CompositionsGenres
            .Where(cg => cg.CompositionId.ToString() == compositionId)
            .ToListAsync();

        dbContext.CompositionsGenres.RemoveRange(genresToDelete);

        await dbContext.SaveChangesAsync();
    }
}
