using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcUserRepository: IUserRepository
{
    private readonly AppContext ctx;
    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }


    public async Task<User> AddUserAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateUserAsync(User user)
    {
        if (!ctx.Users.Any(u => u.UserId == user.UserId))
        {
            throw new InvalidOperationException("User does not exist");
        }
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        User? existingUser = await ctx.Users.SingleOrDefaultAsync(p=>p.UserId == id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("User does not exist");
        }
        ctx.Users.Remove(existingUser);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int userId)
    {
        return await ctx.Users.SingleOrDefaultAsync(u => u.UserId == userId) ?? throw new InvalidOperationException();
    }

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }

    public Task<User?> GetUserByUsernameAndPasswordAsync(string? username, string? password)
    {
        var user = ctx.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        return Task.FromResult(user);
    }
}