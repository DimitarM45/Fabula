namespace Fabula.Data.Models;

using static Common.ValidationConstants.Shared;
using static Common.ValidationConstants.ApplicationUser;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Users")]

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Followers = new HashSet<ApplicationUser>();
        WrittenCompositions = new HashSet<Composition>();
        FavoriteCompositions = new HashSet<UserFavoriteComposition>();
        FavoriteGenres = new HashSet<UserFavoriteGenre>();
        Ratings = new HashSet<Rating>();
        CreatedLists = new HashSet<List>();
        FollowedLists = new HashSet<UserFollowedList>();
        LikedLists = new HashSet<UserLikedList>();
        WrittenComments = new HashSet<Comment>();
        LikedComments = new HashSet<UserLikedComment>();
    }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("First name of user")]

    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("Last name of user")]

    public string LastName { get; set; } = null!;

    [MaxLength(UrlMaxLength)]
    [Comment("A url which leads to the user's profile picture")]

    public string? ProfilePictureUrl { get; set; }

    [MaxLength(BioMaxLength)]
    [Comment("User bio description")]

    public string? Bio { get; set; }

    [MaxLength(UrlMaxLength)]
    [Comment("User website url (social media/personal website)")]

    public string? WebsiteURL { get; set; } 

    [Required]
    [Comment("User date of birth")]

    public DateTime BirthDate { get; set; }

    public IEnumerable<ApplicationUser> Followers { get; set; }

    public IEnumerable<Composition> WrittenCompositions { get; set; }

    public IEnumerable<UserFavoriteComposition> FavoriteCompositions { get; set; }

    public IEnumerable<UserFavoriteGenre> FavoriteGenres { get; set; }

    public IEnumerable<Rating> Ratings { get; set; }

    public IEnumerable<List> CreatedLists { get; set; }

    public IEnumerable<UserFollowedList> FollowedLists { get; set; }

    public IEnumerable<UserLikedList> LikedLists { get; set; }

    public IEnumerable<Comment> WrittenComments { get; set; }

    public IEnumerable<UserLikedComment> LikedComments { get; set; }
}
