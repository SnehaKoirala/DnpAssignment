using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    
    public User User { get; set; } // Navigation Property
    public List<Comment> Comments { get; set; } // Navigation Property

    private Post()
    {
        
    } 
    // Static factory method
    public static Post Create(string title, string body, int userId)
    {
        return new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        };
    }
}