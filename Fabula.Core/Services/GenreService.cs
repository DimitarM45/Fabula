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
}
