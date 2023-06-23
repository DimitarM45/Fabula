namespace Fabula.Data.Models;

using System.ComponentModel.DataAnnotations;

public class UsersLikedStories
{
    [Required]

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    [Required]

    public Guid StoryId { get; set; }

    public Story Story { get; set; } = null!;
}
