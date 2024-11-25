using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    
    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public  async Task UpdateCommentAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.CommentId == comment.CommentId)))
        {
            throw new NotFoundException($"Comment with id {comment.CommentId} not found");
        }

        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.CommentId == id);
        if (existing == null)
        {
            throw new NotFoundException($"Comment with id {id} not found");
        }
        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int commentId)
    {
        return await ctx.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId) ?? throw new InvalidOperationException();
    }

    public IQueryable<Comment> GetMany()
    {
        return ctx.Comments.AsQueryable();
    }
}