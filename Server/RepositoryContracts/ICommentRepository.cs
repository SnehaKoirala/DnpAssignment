using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
    Task<Comment> GetSingleAsync(int commentId);
    IQueryable<Comment> GetMany();
}