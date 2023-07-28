namespace Fabula.Core.Contracts;

using Web.ViewModels.Comment;

public interface ICommentService
{
    Task<IEnumerable<CommentCompositionViewModel>> GetForPreviewByIdAsync(string compositionId);

    Task<IEnumerable<CommentViewModel>> GetAllByIdAsync(string compositionId);

    Task<bool> DeleteByIdAsync(string commentId);
}
