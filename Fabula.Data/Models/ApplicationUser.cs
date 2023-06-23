namespace Fabula.Data.Models;

using static Common.ValidationConstants.Shared;
using static Common.ValidationConstants.ApplicationUser;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Users")]

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Friends = new HashSet<ApplicationUser>();
        WrittenStories = new HashSet<Story>();
        LikedStories = new HashSet<UsersLikedStories>();
    }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("First name of user")]

    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("Last name of user")]

    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment("A url which leads to the user's profile picture")]

    public string ProfilePictureUrl { get; set; } = null!;

    [MaxLength(BioMaxLength)]
    [Comment("User bio description")]

    public string? Bio { get; set; } = null!;

    public IEnumerable<ApplicationUser> Friends { get; set; }

    public IEnumerable<Story> WrittenStories { get; set; }

    public IEnumerable<UsersLikedStories> LikedStories { get; set; }
}
