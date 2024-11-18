namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Body { get; set; }
    
    
    public Post Post { get; set; } //Navigation Property 
    public User User { get; set; } // Naviagation Property 

    private Comment()
    {
        
    } 
    // Static factory method
    public static Comment Create(string body, int postId, int userId)
    {
        return new Comment
        {
            Body = body,
            PostId = postId,
            UserId = userId
        };
    }
}
