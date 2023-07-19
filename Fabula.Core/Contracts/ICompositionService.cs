namespace Fabula.Core.Contracts;

using Web.ViewModels.Composition;

public interface ICompositionService
{
    Task<IEnumerable<CompositionAllViewModel>> GetAllAsync();

    Task<string> AddAsync(CompositionCreateFormModel formModel, string authorId);

    Task<CompositionReadViewModel> GetByIdAsync(string compositionId);

    Task DeleteById(string compositionId);
}
