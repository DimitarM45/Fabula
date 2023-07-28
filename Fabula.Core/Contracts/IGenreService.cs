namespace Fabula.Core.Contracts;

using Web.ViewModels.Genre;

public interface IGenreService
{
    Task<IEnumerable<AllGenreViewModel>> GetAllAsync();

    Task<IEnumerable<GenreViewModel>> GetAllForSelectAsync();

    Task<IEnumerable<GenreViewModel>> GetByIdAsync(string compositionId);

    Task<IEnumerable<int>> GetIdsAsync(string compositionId);

    Task UpdateGenresAsync(string compositionId, IEnumerable<int> genreIds);
}
