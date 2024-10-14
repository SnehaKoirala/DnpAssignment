namespace ApiContracts.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string? Content { get; set; }
}