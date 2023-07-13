namespace Fabula.Core.Contracts;

using Web.ViewModels.Composition;

public interface ICompositionService
{
    Task<IEnumerable<CompositionAllViewModel>> GetAllAsync();

    Task AddAsync(CompositionCreateFormModel formModel, string authorId);
}
