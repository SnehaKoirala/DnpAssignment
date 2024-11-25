using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcPostRepository : IPostRepository
{
   private readonly AppContext ctx;

   public EfcPostRepository(AppContext ctx)
   {
      this.ctx = ctx;
   }
   
   public async Task<Post> AddAsync(Post post)
   {
      EntityEntry<Post> entityEntry = await ctx.Posts.AddAsync(post);
      await ctx.SaveChangesAsync();
      return entityEntry.Entity;
   }

   public async Task UpdateAsync(Post post)
   {
      if (!(await ctx.Posts.AnyAsync(p => p.PostId == post.PostId)))
      {
         throw new NotFoundException($"Post with id {post.PostId} not found.");
      }

      ctx.Posts.Update(post);
      await ctx.SaveChangesAsync();
   }

   public  async Task DeleteAsync(int id)
   {
      Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.PostId == id);
      if (existing == null)
      {
         throw new NotFoundException($"Post with id {id} not found");
      }

      ctx.Posts.Remove(existing);
      await ctx.SaveChangesAsync();
   }

   public async Task<Post> GetSingleAsync(int id)
   {
      return await ctx.Posts.SingleOrDefaultAsync(p => p.PostId == id) ?? throw new InvalidOperationException();
   }

   public IQueryable<Post> GetMany()
   {
      return ctx.Posts.AsQueryable();
   }
}