using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories.Repositories;


public class EfcCommentRepository: ICommentRepository
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


    public async Task UpdateCommentAsync(Comment comment)
    {
        if (!ctx.Comments.Any(c => c.CommentId == comment.CommentId))
        {
            throw new InvalidOperationException("Comment does not exist");
        }

        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {

        Comment? existingComment = await ctx.Comments.SingleOrDefaultAsync(p=>p.CommentId == id);
        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment does not exist");
        }
        ctx.Comments.Remove(existingComment);

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