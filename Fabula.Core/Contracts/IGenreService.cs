namespace Fabula.Core.Contracts;

using Web.ViewModels.Genre;

public interface IGenreService
{
    Task<IEnumerable<AllGenreViewModel>> GetAllAsync();
}
