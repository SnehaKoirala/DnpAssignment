namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public User(string username, string password, int userId)
    {
        UserName = username;
        Password = password;
        UserId = userId;
    }
}