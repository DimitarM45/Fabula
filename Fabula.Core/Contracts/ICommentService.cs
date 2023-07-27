namespace Fabula.Core.Contracts;

using Web.ViewModels.Comment;

public interface ICommentService
{
    Task<IEnumerable<CommentViewModel>> GetAllByIdAsync(string compositionId);
}
