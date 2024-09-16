using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public CreateCommentView(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task AddCommentAsync()
    {
        Console.WriteLine("Enter the post ID to comment on:");
        int postId = int.Parse(Console.ReadLine());

        var post = await postRepository.GetSingleAsync(postId);
        if (post == null)
        {
            Console.WriteLine("Post not found.");
            return;
        }

        Console.WriteLine("Enter your user ID:");
        int userId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter your comment:");
        string? commentText = Console.ReadLine();

        Comment newComment = new Comment(commentText, postId, userId);

        Comment createdComment = await commentRepository.AddCommentAsync(newComment);

        if (createdComment != null)
        {
            Console.WriteLine("Comment Added Successfully:");
            Console.WriteLine($"Post ID: {createdComment.PostId}");
            Console.WriteLine($"User ID: {createdComment.UserId}");
            Console.WriteLine($"Comment: {createdComment.Body}");
        }
        else
        {
            Console.WriteLine("Failed to add the comment. Please try again.");
        }
    }
}