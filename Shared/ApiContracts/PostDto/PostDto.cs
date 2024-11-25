using ApiContracts.Comment;

namespace ApiContracts.Post;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public int UserId { get; set; }
<<<<<<< HEAD
    
    // Add Author property of type UserDto
    public UserDto Author { get; set; } 

    // Add Comments property, which is a list of CommentDto
    public List<CommentDto> Comments { get; set; } 
=======
    public UserDto? Author { get; set; }
    public List<CommentDto> Comments { get; set; }
>>>>>>> 9b41c88e1eac0d347d5f99f743020e678ac6355c
}

   