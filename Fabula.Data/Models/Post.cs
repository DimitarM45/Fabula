namespace Fabula.Data.Models;

using static Common.ValidationConstants.Post;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Posts made by users")]

public class Post
{
    public Post()
    {
        Id = Guid.NewGuid();

        Likes = new HashSet<UsersLikedPosts>();
        Comments = new HashSet<Comment>();
    }

    [Comment("Id of the post")]

    public Guid Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment("Title of the post")]

    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment("Content of the post")]

    public string Content { get; set; } = null!;

    [Required]
    [Comment("Id of post author")]

    public string AuthorId { get; set; } = null!;

    public ApplicationUser Author { get; set; } = null!;

    [Required]
    [Comment("Date of publishing")]

    public DateTime PublishedOn { get; set; }

    [Comment("Date and time of deletion of the post. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a post has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

    public IEnumerable<Comment> Comments { get; set; }

    public IEnumerable<UsersLikedPosts> Likes { get; set; }
}
