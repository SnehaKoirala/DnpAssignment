namespace ApiContracts.Post;

public class UpdatePostDto
{
    public required int PostId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required int UserId { get; set; }
    
}