﻿namespace Fabula.Web.ViewModels.Comment;

using Fabula.Web.ViewModels.User;

public class CommentViewModel
{
    public string? Id { get; set; }

    public string? Content { get; set; }

    public UserViewModel? Author { get; set; }

    public int Likes { get; set; }

    public string? CompositionId { get; set; }

    public DateTime PublishedOn { get; set; }
}
