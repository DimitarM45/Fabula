namespace Fabula.Core.Services;

using Data;
using Contracts;
using Web.ViewModels.User;
using Web.ViewModels.Comment;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

public class CommentService : ICommentService
{
    private readonly FabulaDbContext dbContext;

    public CommentService(FabulaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<CommentViewModel>> GetAllByIdAsync(string compositionId)
    {
        IEnumerable<CommentViewModel> commentViewModels = await dbContext.Comments
            .AsNoTracking()
            .Where(c => c.DeletedOn == null)
            .Select(c => new CommentViewModel()
            {
                Id = c.Id.ToString(),
                Content = c.Content,
                PublishedOn = c.PublishedOn,
                CompositionId = c.CompositionId.ToString(),
                Likes = c.Likes.Count(),
                Author = new UserViewModel()
                {
                    Id = c.AuthorId.ToString(),
                    Username = c.Author.UserName,
                    ProfilePictureUrl = c.Author.ProfilePictureUrl
                },
                Replies = c.Replies.Where(r => r.DeletedOn == null)
                    .Select(r => new CommentViewModel()
                    {
                        Id = r.Id.ToString(),
                        Content = r.Content,
                        PublishedOn = r.PublishedOn,
                        CompositionId = r.CompositionId.ToString(),
                        Likes = c.Likes.Count(),
                        Author = new UserViewModel()
                        {
                            Id = c.AuthorId.ToString(),
                            Username = c.Author.UserName,
                            ProfilePictureUrl = c.Author.ProfilePictureUrl
                        }
                    })
                    .ToList()
            })
            .ToListAsync();

        return commentViewModels;
    }
}
