namespace Fabula.Core.Contracts;

using Web.ViewModels.Genre;

public interface IGenreService
{
    Task<IEnumerable<AllGenreViewModel>> GetAllAsync();

    Task<IEnumerable<GenreViewModel>> GetAllForSelectAsync();

    Task<IEnumerable<GenreViewModel>> GetByIdAsync(string compositionId);

    Task<IEnumerable<GenreViewModel>> GetForUserAsync(string userId);

    Task AddToCompositionAsync(string compositionId, IEnumerable<int> genreIds);

    Task RemoveFromCompositionAsync(string compositionId, IEnumerable<int> genreIds);
}
