namespace Fabula.Core.Contracts;

using Web.ViewModels.Comment;

public interface ICommentService
{
    Task<IEnumerable<CommentCompositionViewModel>> GetForPreviewByIdAsync(string compositionId);

    Task<CommentsAllViewModel?> GetAllByIdAsync(string compositionId);

    Task<bool> DeleteByIdAsync(string commentId);
}
