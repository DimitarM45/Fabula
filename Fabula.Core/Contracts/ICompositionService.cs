﻿namespace Fabula.Core.Contracts;

using Enums;
using Fabula.Web.ViewModels.Admin.Composition;
using ServiceModels;
using Web.ViewModels.Composition;

public interface ICompositionService
{
    Task<CompositionQueryModel> GetAllAsync(IEnumerable<int>? selectedGenres = null,
        string? userId = null,
        string? searchTerm = null,
        int currentPage = 1,
        int compositionsPerPage = 1,
        DateSorting dateSorting = DateSorting.Newest,
        RatingSorting ratingSorting = RatingSorting.BestRated);

    Task<string> AddAsync(CompositionFormModel formModel, string authorId);

    Task<CompositionReadViewModel?> GetByIdAsync(string compositionId);

    Task<bool> DeleteByIdAsync(string compositionId);

    Task<CompositionFormModel?> GetForEditAsync(string compositionId);

    Task UpdateAsync(CompositionFormModel formModel);

    Task<string?> GetRandomIdAsync();

    Task<bool> RestoreByIdAsync(string compositionId);

    Task<IEnumerable<CompositionProfileViewModel>> GetAllForUserAsync(string userId);

    Task<int> GetCountAsync();

    Task<IEnumerable<CompositionDashboardViewModel>> GetAllForAdminDashboardAsync();
}
