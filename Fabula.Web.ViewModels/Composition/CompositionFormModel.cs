﻿namespace Fabula.Web.ViewModels.Composition;

using Genre;
using Attributes;
using Attributes.Enums;

using static Common.ValidationConstants.Shared;
using static Common.Messages.ErrorMessages.Shared;
using static Common.ValidationConstants.Composition;
using static Common.Messages.ErrorMessages.Composition;

using System.ComponentModel.DataAnnotations;

public class CompositionFormModel
{
    public CompositionFormModel()
    {
        Genres = new HashSet<int>();
        GenresToSelect = new HashSet<GenreViewModel>();
    }

    public string? Id { get; set; }

    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string Title { get; set; } = null!;

    [Url(ErrorMessage = InvalidStringErrorMessage)]
    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(UrlMaxLength, MinimumLength = UrlMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string CoverUrl { get; set; } = null!;

    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string Content { get; set; } = null!;

    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(SynopsisMaxLength, MinimumLength = SynopsisMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string Synopsis { get; set; } = null!;

    public string? AuthorId { get; set; }

    public bool HasAdultContent { get; set; }

    [ElementCount(1, CountStrategy.Minimum,
        ErrorMessage = GenreCountErrorMessage)]

    public IEnumerable<int> Genres { get; set; }

    public IEnumerable<GenreViewModel> GenresToSelect { get; set; }
}
