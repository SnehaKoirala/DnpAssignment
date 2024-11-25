using ApiContracts.Comment;

namespace ApiContracts.Post;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public int UserId { get; set; }
    
    // Add Author property of type UserDto
    public UserDto Author { get; set; } 

    // Add Comments property, which is a list of CommentDto
    public List<CommentDto> Comments { get; set; } 

}

   