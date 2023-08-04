﻿namespace Fabula.Core.Contracts;

using ServiceModels;
using Web.ViewModels.Composition;

public interface ICompositionService
{
    Task<IEnumerable<CompositionViewModel>> GetAllAsync();

    Task<CompositionQueryModel> All(string category = null,
        string searchTerm = null);

    Task<string> AddAsync(CompositionFormModel formModel, string authorId);

    Task<CompositionReadViewModel> GetByIdAsync(string compositionId);

    Task<bool> DeleteByIdAsync(string compositionId);

    Task<CompositionFormModel?> GetForEditAsync(string compositionId);

    Task UpdateAsync(CompositionFormModel formModel);

    Task<string?> GetRandomIdAsync();

    Task<bool> RestoreByIdAsync(string compositionId);

    Task<IEnumerable<CompositionProfileViewModel>> GetAllForUserAsync(string userId);
}
