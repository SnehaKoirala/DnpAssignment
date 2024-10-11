namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public User(string username, string password)
    {
        UserName = username;
        Password = password;
    }
}