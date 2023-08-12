namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.Genre;

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

    public Task<IEnumerable<string>> GetAllNamesAsync()
    {
        throw new NotImplementedException();
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
}
