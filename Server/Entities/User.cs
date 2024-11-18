namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string Password { get; set; } 
    
    public List<Post> Posts { get; set; } //Navigation Property
    public List<Comment> Comments { get; set; } // Navigation Property
    
    private User()
    {
        
    } 
    // static method 
    public static User Create(string userName, string password)
    {
        return new User
        {
            UserName = userName,
            Password = password
        };
    }

}