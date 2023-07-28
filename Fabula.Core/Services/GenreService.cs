namespace Fabula.Core.Services;

using Data;
using Contracts;
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
                CompositionCount = g.Compositions.Count(c => c.DeletedOn == null)
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
        IEnumerable<GenreViewModel> genreViewModels = await dbContext.Genres
            .AsNoTracking()
            .Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = g.Name,
            })
            .ToListAsync();

        return genreViewModels;
    }

    public async Task<IEnumerable<int>> GetIdsAsync(string compositionId)
    {
        IEnumerable<int> ids = await dbContext.Compositions
            .AsNoTracking()
            .Select(c => c.Genres.Count)
            .ToListAsync();

        return ids;
    }

    public Task UpdateGenresAsync(string compositionId, IEnumerable<int> genreIds)
    {
        throw new NotImplementedException();
    }
}
