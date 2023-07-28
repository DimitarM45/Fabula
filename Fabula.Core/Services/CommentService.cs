namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
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
                Replies = c.Replies.Where(r => r.DeletedOn == null)
                    .Select(r => new CommentViewModel()
                    {
                        Id = r.Id.ToString(),
                        Content = r.Content,
                        PublishedOn = r.PublishedOn,
                        CompositionId = r.CompositionId.ToString(),
                        Likes = c.Likes.Count()
                    })
                    .ToList()
            })
            .ToListAsync();

        return commentViewModels;
    }

    public async Task<IEnumerable<CommentCompositionViewModel>> GetForPreviewByIdAsync(string compositionId)
    {
        IEnumerable<CommentCompositionViewModel> commentViewModels = await dbContext.Comments
            .AsNoTracking()
            .Where(c => c.DeletedOn == null)
            .Select(c => new CommentCompositionViewModel()
            {
                Id = c.Id.ToString(),
                Content = c.Content,
                CompositionId = c.CompositionId.ToString(),
                Likes = c.Likes.Count(),
                PublishedOn = c.PublishedOn,
                Replies = c.Replies.Count()
            })
            .ToListAsync();

        return commentViewModels;
    }

    public async Task<bool> DeleteByIdAsync(string commentId)
    {
        Comment? comment = await dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id.ToString() == commentId && c.DeletedOn == null);

        bool isSuccessful = false;

        if (comment != null)
        {
            comment.DeletedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();

            isSuccessful = true;
        }

        return isSuccessful;
    }
}
