namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
}