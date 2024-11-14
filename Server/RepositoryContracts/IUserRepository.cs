using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User> GetSingleAsync(int userId);
    IQueryable<User> GetMany();
    Task<User?> GetUserByUsernameAndPasswordAsync(string? username, string? password);
}