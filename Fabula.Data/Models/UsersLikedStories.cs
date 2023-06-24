﻿namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the stories they've liked")]

public class UsersLikedStories
{
    [Required]
    [Comment("Id of user")]

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of story")]

    public Guid StoryId { get; set; }

    public Story Story { get; set; } = null!;
}
