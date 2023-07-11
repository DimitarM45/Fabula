﻿namespace Fabula.Web.ViewModels.Composition;

public class CompositionAllViewModel
{
    public string? Id { get; set; }

    public string? Title { get; set; } 

    public string? CoverUrl { get; set; } 

    public string? Content { get; set; }

    public string? Synopsys { get; set; }

    public string? AuthorId { get; set; }

    public string? Author { get; set; }

    public DateTime PublishedOn { get; set; }

    public bool HasAdultContent { get; set; }

    public double Rating { get; set; }
}
