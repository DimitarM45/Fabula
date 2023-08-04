namespace Fabula.Core.ServiceModels;

using Enums;
using Web.ViewModels.Composition;

using System.ComponentModel.DataAnnotations;

public class CompositionQueryModel
{
    public CompositionQueryModel()
    {
        Genres = new HashSet<string>();
        Compositions = new HashSet<CompositionViewModel>();
    }

    public int CompositionsPerPage = 3;

    public string? Genre { get; set; }

    [Display(Name = "Search by term")]

    public string? SearchTerm { get; set; }

    public ResultSorting Sorting { get; set; }

    public int CurrentPage { get; set; } = 1;

    public IEnumerable<string> Genres { get; set; }

    public int CompositionsCount { get; set; }

    public IEnumerable<CompositionViewModel> Compositions { get; set; }
}
