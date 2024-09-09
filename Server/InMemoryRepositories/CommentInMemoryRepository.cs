using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    public Task<Comment> AddComment(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task UpdateComment(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteComment(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> GetSingleAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetMany()
    {
        throw new NotImplementedException();
    }
}