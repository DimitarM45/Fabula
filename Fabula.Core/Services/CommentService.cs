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

    public async Task<CommentsAllViewModel?> GetAllByIdAsync(string compositionId)
    {
        IEnumerable<CommentViewModel> commentViewModels = await dbContext.Comments
            .AsNoTracking()
            .Where(c => c.DeletedOn == null && c.CompositionId.ToString() == compositionId)
            .Select(c => new CommentViewModel()
            {
                Id = c.Id.ToString(),
                Content = c.Content,
                PublishedOn = c.PublishedOn,
                Likes = c.Likes.Count(),
                Replies = c.Replies.Where(r => r.DeletedOn == null)
                    .Select(r => new CommentViewModel()
                    {
                        Id = r.Id.ToString(),
                        Content = r.Content,
                        PublishedOn = r.PublishedOn,
                        Likes = c.Likes.Count()
                    })
                    .ToList()
            })
            .ToListAsync();

        Composition? composition = await dbContext.Compositions
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id.ToString() == compositionId);

        if (composition == null)
            return null;

        string compositionTitle = composition.Title;

        CommentsAllViewModel commentsAllViewModel = new CommentsAllViewModel()
        {
            CompositionId = compositionId,
            CompositionTitle = compositionTitle
        };

        return commentsAllViewModel;
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
