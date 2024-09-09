using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddComment(Comment comment);
    Task UpdateComment(Comment comment);
    Task DeleteComment(int id);
    Task<Comment> GetSingleAsync(int commentId);
    IQueryable<Comment> GetMany();
}