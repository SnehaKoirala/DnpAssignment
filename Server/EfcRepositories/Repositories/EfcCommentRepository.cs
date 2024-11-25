using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

<<<<<<< HEAD
public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

=======
public class EfcCommentRepository: ICommentRepository
{
    private readonly AppContext ctx;
>>>>>>> 9b41c88e1eac0d347d5f99f743020e678ac6355c
    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
<<<<<<< HEAD
    
=======
>>>>>>> 9b41c88e1eac0d347d5f99f743020e678ac6355c
    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

<<<<<<< HEAD
    public  async Task UpdateCommentAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.CommentId == comment.CommentId)))
        {
            throw new NotFoundException($"Comment with id {comment.CommentId} not found");
        }

=======
    public async Task UpdateCommentAsync(Comment comment)
    {
        if (!ctx.Comments.Any(c => c.CommentId == comment.CommentId))
        {
            throw new InvalidOperationException("Comment does not exist");
        }
>>>>>>> 9b41c88e1eac0d347d5f99f743020e678ac6355c
        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {
<<<<<<< HEAD
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.CommentId == id);
        if (existing == null)
        {
            throw new NotFoundException($"Comment with id {id} not found");
        }
        ctx.Comments.Remove(existing);
=======
        Comment? existingComment = await ctx.Comments.SingleOrDefaultAsync(p=>p.CommentId == id);
        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment does not exist");
        }
        ctx.Comments.Remove(existingComment);
>>>>>>> 9b41c88e1eac0d347d5f99f743020e678ac6355c
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