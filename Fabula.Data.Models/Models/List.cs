namespace Fabula.Data.Models;

using static Common.ValidationConstants.List;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Reading lists")]

public class List
{
    public List()
    {
        Compositions = new HashSet<Composition>();
        Likes = new HashSet<UserLikedList>();
        Followers = new HashSet<UserFollowedList>();
    }

    [Required]
    [Comment("Id of list")]

    public Guid Id { get; set; }

    [Required]
    [Comment("Title of list")]
    [MaxLength(TitleMaxLength)]

    public string Title { get; set; } = null!;

    [Required]
    [Comment("Description of list")]
    [MaxLength(DescriptionMaxLength)]

    public string Description { get; set; } = null!;

    [Required]
    [Comment("Date of creation")]

    public DateTime CreatedOn { get; set; }

    [Comment("Date and time of deletion of the list. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a list has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

    [Required]
    [Comment("Adult content flag of the list")]

    public bool hasAdultContent { get; set; }

    [Required]
    [Comment("Id of creator")]

    public Guid CreatorId { get; set; }

    [Required]

    public ApplicationUser Creator { get; set; } = null!;

    public IEnumerable<Composition> Compositions { get; set; }

    public IEnumerable<UserLikedList> Likes { get; set; }

    public IEnumerable<UserFollowedList> Followers { get; set; }
}
