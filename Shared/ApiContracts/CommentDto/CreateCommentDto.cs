namespace ApiContracts.Comment;

public class CreateCommentDto
{
    public required int UserId { get; set; }
    public required int PostId { get; set; }
    public required string Content { get; set; }
}