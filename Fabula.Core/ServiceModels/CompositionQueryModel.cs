﻿namespace Fabula.Core.ServiceModels;

using Enums;
using Web.ViewModels.Genre;
using Web.ViewModels.Composition;

using static Common.GlobalConstants;

public class CompositionQueryModel
{
    public CompositionQueryModel()
    {
        Genres = new HashSet<GenreViewModel>();
        Compositions = new HashSet<CompositionViewModel>();
        SelectedGenres = new HashSet<int>();
    }

    public int CompositionsPerPage { get; set; } = CompositionsPerPageCount;

    public string? SearchTerm { get; set; }

    public string? UserId { get; set; }

    public string? Username { get; set; }

    public DateSorting DateSorting { get; set; }

    public RatingSorting RatingSorting { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int CompositionsCount { get; set; } 

    public IEnumerable<GenreViewModel> Genres { get; set; }

    public IEnumerable<int> SelectedGenres { get; set; }

    public IEnumerable<CompositionViewModel> Compositions { get; set; }
}
