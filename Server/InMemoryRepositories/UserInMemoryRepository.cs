using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = new();

    public UserInMemoryRepository()
    {
        _ = AddUserAsync(new User("trmo", "1234", 0)).Result;
        _ = AddUserAsync(new User("mivi", "4321", 0)).Result;
        _ = AddUserAsync(new User("jknr", "1243", 0)).Result;
        _ = AddUserAsync(new User("kasr", "2143", 0)).Result;
    }

    public Task<User> AddUserAsync(User user)
    {
        if (users.Any(u => u.Username == user.Username))
        {
            throw new InvalidOperationException($"Username '{user.Username}' is already taken.");
        }

        user.UserId = users.Any() ? users.Max(u => u.UserId) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateUserAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.UserId == user.UserId);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.UserId}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.UserId == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int userId)
    {
        User? user = users.SingleOrDefault(u => u.UserId == userId);
        if (user is null)
        {
            throw new InvalidOperationException($"User with Id '{userId}' not found");
        }

        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    public Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password)
    {
        User? user = users.SingleOrDefault(u => u.Username == username && u.Password == password);
        return Task.FromResult(user);
    }
}