using ApiContracts.Comment;

namespace ApiContracts.Post;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public int UserId { get; set; }
    public UserDto? Author { get; set; }
    public List<CommentDto> Comments { get; set; }
}
   