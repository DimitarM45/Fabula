namespace Fabula.Core.Contracts;

using Web.ViewModels.Composition;

public interface ICompositionService
{
    Task<IEnumerable<CompositionAllViewModel>> GetAllAsync();

    Task<string> AddAsync(CompositionFormModel formModel, string authorId);

    Task<CompositionReadViewModel> GetByIdAsync(string compositionId);

    Task DeleteById(string compositionId);

    Task<CompositionFormModel?> GetForEditAsync(string compositionId);

    Task UpdateAsync(CompositionFormModel formModel);

    Task<string> GetRandomIdAsync();
}
