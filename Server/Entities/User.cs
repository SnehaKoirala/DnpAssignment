namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public User(string username, string password, int userId)
    {
        Username = username;
        Password = password;
        UserId = userId;
    }
}